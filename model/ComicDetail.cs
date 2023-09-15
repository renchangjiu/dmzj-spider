namespace dmzj_spider.model;

public class ComicDetail {

    public int errno { get; set; }
    public string errmsg { get; set; }
    public Data data { get; set; }

    public class Data {

        public ComicInfo comicInfo { get; set; }

    }

    public class ComicInfo {

        public int id { get; set; }
        public string title { get; set; }
        public string aliasName { get; set; }
        public string realName { get; set; }
        public StatusTagList[] statusTagList { get; set; }
        public CateTagList[] cateTagList { get; set; }
        public AuthorsTagList[] authorsTagList { get; set; }
        public ZoneTagList[] zoneTagList { get; set; }
        public ThemeTagList[] themeTagList { get; set; }
        public string comicPy { get; set; }
        public int lastUpdateTime { get; set; }
        public string lastUpdateChapterName { get; set; }
        public int lastUpdateChapterId { get; set; }
        public int hotNum { get; set; }
        public int hitNum { get; set; }
        public int subNum { get; set; }
        public int viewPointNum { get; set; }
        public string description { get; set; }
        public string comicNotice { get; set; }
        public Chapter[] chapterList { get; set; }
        public bool isFee { get; set; }
        public bool canRead { get; set; }
        public string cover { get; set; }
        public int sumChapter { get; set; }
        public int sumSource { get; set; }

    }

    public class StatusTagList {

        public int tagId { get; set; }
        public string tagName { get; set; }
        public string tagPy { get; set; }

    }

    public class CateTagList {

        public int tagId { get; set; }
        public string tagName { get; set; }
        public string tagPy { get; set; }

    }

    public class AuthorsTagList {

        public int tagId { get; set; }
        public string tagName { get; set; }
        public string tagPy { get; set; }

    }

    public class ZoneTagList {

        public int tagId { get; set; }
        public string tagName { get; set; }
        public string tagPy { get; set; }

    }

    public class ThemeTagList {

        public int tagId { get; set; }
        public string tagName { get; set; }
        public string tagPy { get; set; }

    }

    public class Chapter {

        public string title { get; set; }
        public ChapterData[] data { get; set; }

    }

    public class ChapterData {

        public int chapter_id { get; set; }
        public string chapter_title { get; set; }
        public int updatetime { get; set; }
        public int filesize { get; set; }
        public int chapter_order { get; set; }
        public bool is_fee { get; set; }

    }

}