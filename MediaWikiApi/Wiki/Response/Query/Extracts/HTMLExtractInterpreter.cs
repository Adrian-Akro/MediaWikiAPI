using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MediaWikiApi.Wiki.Response.Query.Extracts {
    public class HTMLExtractInterpreter : IExtractInterpreter {

        public HTMLExtractInterpreter(string rawContent) {
            Sections = new List<Section>();
            if (IsHtml(rawContent)) {
                InterpretAsHtml(rawContent);
            }
        }
        public List<Section> Sections { get; set; }

        public static bool IsHtml(string rawContent) {
            Regex tagRegex = new Regex(@"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
            return tagRegex.IsMatch(rawContent);
        }

        private void InterpretAsHtml(string rawContent) {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawContent);
            IEnumerator<HtmlNode> enumerator = doc.DocumentNode.Descendants().GetEnumerator();
            if (enumerator.MoveNext()) {
                HtmlNode node = enumerator.Current;
                while (node != null) {
                    Sections.Add(RecursiveHtmlInterpretation(ref node, new Section(), 2));
                }
            }
        }

        private Section RecursiveHtmlInterpretation(ref HtmlNode node, Section section, int depth, bool returnOnNextHeading = false) {
            Match match = new Regex(@"[h]([0-9]+)").Match(node.Name);
            if (match.Success && match.Groups.Count > 1) {
                int headingNumber = int.Parse(match.Groups[1].Value);
                if (headingNumber == depth) {
                    if (returnOnNextHeading) {
                        return section;
                    }
                    section.Title = CleanText(node.InnerText);
                } else if (headingNumber > depth) {
                    section.Subsections.Add(
                        RecursiveHtmlInterpretation(ref node, new Section(), depth + 1)
                        );
                    return RecursiveHtmlInterpretation(ref node, section, depth, true);
                } else {
                    return section;
                }
            } else {
                section.Content += InterpretNonHeadingText(node);
            }
            node = node.NextSibling;
            if (node != null) {
                return RecursiveHtmlInterpretation(ref node, section, depth, true);
            } else {
                return section;
            }
        }


        private string InterpretNonHeadingText(HtmlNode node) {
            StringBuilder strBuilder = new StringBuilder();
            switch (node.Name) {
                case "div":
                case "ol":
                case "ul":
                    foreach (HtmlNode childNode in node.ChildNodes) {
                        string subText = InterpretNonHeadingText(childNode);
                        strBuilder.Append(subText);
                        if (!subText.EndsWith('\n')) {
                            strBuilder.Append('\n');
                        }
                    }
                    break;
                default:
                    strBuilder.Append(node.InnerText);
                    break;
            }
            return CleanText(strBuilder.ToString());
        }

        private string RemoveBracketedNumbers(string text) {
            Regex bracketRegex = new Regex(@"([\[][0-9]+[\]])+");
            return bracketRegex.Replace(text, "");
        }

        private string RemoveLeadingNewLines(string text) {
            return text
                .TrimStart('\n')
                .TrimStart();
        }

        private string CleanText(string text) {
            text = RemoveLeadingNewLines(text);
            text = RemoveBracketedNumbers(text);
            return text;
        }
    }
}
