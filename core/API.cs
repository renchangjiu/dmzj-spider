using System.Text;
using System.Text.Json;
using dmzj_spider.model;

namespace dmzj_spider.core;

public class API {

    private readonly HttpClient client;
    private readonly string comicPy;
    private readonly string uid;
    private readonly string dest;

    /// <summary>
    ///
    /// </summary>
    /// <param name="comicPy">漫画的拼音名称</param>
    /// <param name="uid">user id</param>
    /// <param name="dest">输出目录, 默认为桌面</param>
    public API(string comicPy, string uid, string dest = "") {
        this.client = new HttpClient();

        this.comicPy = comicPy;
        this.uid = uid;
        this.dest = string.IsNullOrWhiteSpace(dest)
            ? Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            : dest;
    }

    public void doo() {
        (ComicDetail, string) comicDetail = getComicDetail();
        ComicDetail comic = comicDetail.Item1;
        string result = comicDetail.Item2;

        ComicDetail.ComicInfo comicInfo = comic.data.comicInfo;
        DirectoryInfo destComicDir = new(Path.Combine(dest, comicPy));
        destComicDir.Create();

        // write comic detail json file
        FileStream fs = File.OpenWrite(Path.Combine(destComicDir.FullName, nameof(ComicDetail) + ".json"));
        fs.Write(Encoding.UTF8.GetBytes(result));
        fs.Flush(true);
        fs.Close();

        ComicDetail.Chapter[] chapters = comicInfo.chapterList;

        foreach (var c in chapters) {
            DirectoryInfo chapterDir = new(Path.Combine(destComicDir.FullName, c.title));
            chapterDir.Create();

            ComicDetail.ChapterData[] chapterDatas = c.data;
            foreach (var d in chapterDatas) {
                DirectoryInfo lessonDir =
                    new(Utils.replaceIllegalChar(Path.Combine(chapterDir.FullName, d.chapter_title)));
                lessonDir.Create();

                ChapterDetail cd = getChapterDetail(d);
                ChapterDetail.ChapterInfo chapterInfo = cd.data.chapterInfo;
                string[] pageUrls = chapterInfo.page_url;
                for (int i = 0; i < pageUrls.Length; i++) {
                    string pageUrl = pageUrls[i];
                    string ext = Path.GetExtension(pageUrl);
                    string name = i.ToString().PadLeft(4, '0') + ext;
                    string destImage = Path.Combine(lessonDir.FullName, name);
                    if (File.Exists(destImage)) {
                        Console.WriteLine($"image exist, skip: {c.title}: {chapterInfo.title}: {destImage}");
                        continue;
                    }

                    downloadImage(pageUrl, destImage);
                    Console.WriteLine($"{c.title}: {chapterInfo.title}: {destImage}");
                    Thread.Sleep(200);
                }

                Console.WriteLine("-----------------------------------------");
            }
        }
    }


    public (ComicDetail, string) getComicDetail() {
        string url =
            $"https://manhua.idmzj.com/api/v1/comic2/comic/detail?channel=pc&app_name=comic&version=1.0.0&timestamp=1694660906746&uid={uid}&comic_py={comicPy}&page=1&size=50";
        Console.WriteLine("request ComicDetail: " + url);
        HttpResponseMessage resp = client.GetAsync(url).Result;
        string result = resp.Content.ReadAsStringAsync().Result;
        ComicDetail vo = JsonSerializer.Deserialize<ComicDetail>(result)!;
        throwExceptionIfNotOk(vo);
        if (vo.data.comicInfo.chapterList == null) {
            throw new BizException("章节列表为空, 该漫画可能被下架了");
        }

        return (vo, result);
    }


    private ChapterDetail getChapterDetail(ComicDetail.ChapterData c) {
        string url =
            $"https://manhua.idmzj.com/api/v1/comic2/chapter/detail?channel=pc&app_name=comic&version=1.0.0&timestamp=1694660906746&uid={uid}&comic_py={comicPy}&chapter_id={c.chapter_id}";
        Console.WriteLine("request ChapterDetail: " + url);
        HttpResponseMessage resp = client.GetAsync(url).Result;
        string result = resp.Content.ReadAsStringAsync().Result;
        ChapterDetail vo = JsonSerializer.Deserialize<ChapterDetail>(result)!;
        throwExceptionIfNotOk(vo);
        return vo;
    }


    private void downloadImage(string pageUrl, string fullPath) {
        HttpResponseMessage resp = client.GetAsync(pageUrl).Result;
        Stream readAsStream = resp.Content.ReadAsStream();
        using FileStream fs = File.OpenWrite(fullPath);
        readAsStream.CopyTo(fs);
    }

    private static void throwExceptionIfNotOk(BaseVo vo) {
        if (isNotOk(vo)) {
            throw new BizException(vo.errmsg);
        }
    }

    private static bool isNotOk(BaseVo vo) {
        return vo.errno != 0;
    }

}