using Avalonia.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RM_STO_DIPLOM.ViewModels
{
    public class DescriptionWindowModel
    {
        public string Text { get; set; }
        DescriptionWindow descriptionWindow;
        public DescriptionWindowModel(Declaration declaration, DescriptionWindow descriptionWindow_)
        {
            Text = GetDescription(declaration);
            descriptionWindow = descriptionWindow_;
        }

        public string GetDescription(Declaration declaration)
        {
            string desc = "";

            if (File.Exists($"{declaration.Placement}\\description.txt"))
            {

                desc = File.ReadAllText($"{declaration.Placement}\\description.txt");
                return desc;
            }
            else
            {
                return "Выполните задание преподавателя";
            }
        }

        public void CloseWin()
        {
            descriptionWindow.Close();
        }
    }
}
