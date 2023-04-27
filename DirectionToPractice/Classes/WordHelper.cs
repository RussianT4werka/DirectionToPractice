using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectionToPractice.Classes
{
    public class WordHelper
    {
        private FileInfo _fileInfo;

        public WordHelper(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                MessageBox.Show("Файл не найден");
            }
                
        }
        internal bool EditDocument(Dictionary<string, string> items)
        {
            Microsoft.Office.Interop.Word.Application app = null;
            try
            {
                app = new Microsoft.Office.Interop.Word.Application();
                Object file = _fileInfo.FullName;
                Object missing = Type.Missing;
                app.Documents.Open(file);
                foreach(var item in items)
                {
                    Microsoft.Office.Interop.Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    Object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing,
                        Replace: replace) ;
                }

                Object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyyyMMdd HHmmss ") + _fileInfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();
                app.Quit();
                return true;
            }
            catch
            {
                MessageBox.Show("Упс... Кажется что-то пошло не так :)");
            }
            return false;
        }
    }
}
