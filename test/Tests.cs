using dmzj_spider.core;
using dmzj_spider.model;

namespace dmzj_spider.test;

public class Tests {

    public static void test1() {
        try {
            API api = new("jiejiaomozu", "117025832");
            (ComicDetail, string) comicDetail = api.getComicDetail();
            Console.WriteLine("");
        } catch (BizException e) {
            Console.WriteLine(e.Message);
        }
    }

}