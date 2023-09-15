using System.Text.Json;
using dmzj_spider.model;

namespace dmzj_spider.core;

public class API {

    public static void doo() {
        string destDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        ComicDetail comic = getComicDetail();
        var comicInfo = comic.data.comicInfo;
        string comicInfoComicPy = comicInfo.comicPy;
        DirectoryInfo destComicDir = new(Path.Combine(destDir, comicInfoComicPy));
        destComicDir.Create();

        ComicDetail.Chapter[] chapters = comicInfo.chapterList;
        foreach (var c in chapters) {
            DirectoryInfo chapterDir = new(Path.Combine(destComicDir.FullName, c.title));
            chapterDir.Create();

            ComicDetail.ChapterData[] chapterDatas = c.data;
            foreach (var d in chapterDatas) {
                DirectoryInfo lessonDir = new(Path.Combine(chapterDir.FullName, d.chapter_title));
                lessonDir.Create();

                ChapterDetail cd = getChapterDetail(d);
                ChapterDetail.ChapterInfo chapterInfo = cd.data.chapterInfo;
                string[] pageUrls = chapterInfo.page_url;
                for (int i = 0; i < pageUrls.Length; i++) {
                    string pageUrl = pageUrls[i];
                    string ext = Path.GetExtension(pageUrl);
                    string name = i.ToString().PadLeft(4, '0')+ext;
                    string dest = Path.Combine(lessonDir.FullName, name);
                    downloadImage(pageUrl, dest);
                    Console.WriteLine($"{c.title}: {chapterInfo.title}: {dest}");
                    Thread.Sleep(500);
                }

                Console.WriteLine("-----------------------------------------");
            }
        }
    }


    private static ComicDetail getComicDetail() {
        string url =
            "https://manhua.idmzj.com/api/v1/comic2/comic/detail?channel=pc&app_name=comic&version=1.0.0&timestamp=1694660906746&uid=117025832&comic_py=wzyxdjm&page=1&size=50";
        HttpClient client = new();
        HttpResponseMessage resp = client.GetAsync(url).Result;
        string result = resp.Content.ReadAsStringAsync().Result;
        ComicDetail comicDetail = JsonSerializer.Deserialize<ComicDetail>(result);
        return comicDetail;
    }


    private static ChapterDetail getChapterDetail(ComicDetail.ChapterData c) {
        string url =
            $"https://manhua.idmzj.com/api/v1/comic2/chapter/detail?channel=pc&app_name=comic&version=1.0.0&timestamp=1694660906746&uid=117025832&comic_py=wzyxdjm&chapter_id={c.chapter_id}";

        HttpClient client = new();
        HttpResponseMessage resp = client.GetAsync(url).Result;
        string result = resp.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<ChapterDetail>(result);
    }


    private static void downloadImage(string pageUrl, string fullPath) {
        HttpClient client = new();
        HttpResponseMessage resp = client.GetAsync(pageUrl).Result;
        Stream readAsStream = resp.Content.ReadAsStream();
        using FileStream fs = File.OpenWrite(fullPath);
        readAsStream.CopyTo(fs);
    }

}