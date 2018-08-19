using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XMLDocGen
{
    class PageGenerator
    {
        private List<PageData> pages;

        public PageGenerator(List<PageData> _pages)
        {
            pages = _pages;
        }

        public void Generate()
        {
            for (int i = 0; i < pages.Count; i++)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(pages[i].path));
                File.WriteAllText(Program.OutFolder + "/" + pages[i].path, pages[i].content);
            }

            GenerateIndex();
        }

        public void GenerateIndex()
        {
            MarkdownBuilder builder = new MarkdownBuilder();

            builder.H1("INDEX");

            for (int i = 0; i < pages.Count; i++)
            {
                builder.Bullet(MarkdownBuilder.CreatePageLink(pages[i].name, pages[i].path));
            }

            File.WriteAllText(Program.OutFolder + "/index.md", builder.Value);
        }

        public List<PageData> CustomPages()
        {
            List<PageData> customPages = new List<PageData>();

            return customPages;
        }
    }
}
