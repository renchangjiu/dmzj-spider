using CommandLine;

namespace dmzj_spider.core;

public class Options {

    [Option('c', "comicPy", Required = true, HelpText = "The phonetic name of the manga.")]
    public required string ComicPy { get; set; }

    [Option('u', "uid", Required = true, HelpText = "User id.")]
    public required string Uid { get; set; }

    [Option('o', "output", Required = false, HelpText = "Output directory, default is desktop directory.")]
    public required string Output { get; set; }

}