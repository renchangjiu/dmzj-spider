using System.Diagnostics.CodeAnalysis;
using CommandLine;
using dmzj_spider.core;
using dmzj_spider.model;

namespace dmzj_spider;

public class Program {

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Options))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ComicDetail))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ChapterDetail))]
    public static void Main(string[] args) {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o => {
                API pi = new(o.ComicPy, o.Uid, o.Output);
                try {
                    pi.doo();
                } catch (BizException e) {
                    Console.WriteLine(e.Message);
                }
            });
    }

}