namespace dmzj_spider.model;

public class ChapterDetail {

    public int errno { get; set; }
    public string errmsg { get; set; }
    public Data data { get; set; }


    public class Data {

        public ChapterInfo chapterInfo { get; set; }

    }

    public class ChapterInfo {

        public int chapter_id { get; set; }
        public int comic_id { get; set; }
        public string title { get; set; }
        public int chapter_order { get; set; }
        public int direction { get; set; }
        public string[] page_url { get; set; }
        public int picnum { get; set; }
        public object page_url_hd { get; set; }
        public Author author { get; set; }

    }

    public class Author {

        public string name { get; set; }
        public int uid { get; set; }
        public string photo { get; set; }

    }

}