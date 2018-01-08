using Asg2_vxc152130;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vxc152130_RebateForm
{
    /**
     * Helper class for RebateForm add, modify, delete operations
     * @author: Vincy Shrine
     */
    static class Helper
    {
        public static void listView_Records_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e, ListView listView_Records)
        {
            e.NewWidth = listView_Records.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        public static void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {

            using (var sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                using (var headerFont = new Font("Microsoft Sans Serif", 10, FontStyle.Bold))
                {
                    e.Graphics.FillRectangle(Brushes.AliceBlue, e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                }
            }
        }

        public static Icon ResizeIcon(Icon icon, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(icon.ToBitmap(), new Rectangle(Point.Empty, size));
            }
            return Icon.FromHandle(bitmap.GetHicon());
        }

        public static bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                // var r = new Regex(@"^(\+)?[\d\s]+$");
                var r = new Regex(@"^\(?([0-9]{3})\)?[-.\s]?([0-9]{3})[-.\s]?([0-9]{4})$");
                return r.IsMatch(Phone);
            }
            catch (Exception)
            {
                //throw;
                return false;
            }
        }

        public static bool IsValidZipCode(string zipCode)
        {
            //string _usZipRegEx = @"^[\d\s]+$";
            string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            bool validZipCode = true;
            if (!Regex.Match(zipCode, _usZipRegEx).Success)
            {
                validZipCode = false;
            }
            return validZipCode;
        }

        public static bool IsValidEmail(string email)
        {
            string emailRegEx = @"^[a-zA-Z_.0-9]+@[a-zA-Z_0-9]+[.]{1}[a-zA-Z]+$";
            bool validEmail = true;
            if (!Regex.Match(email, emailRegEx).Success)
            {
                validEmail = false;
            }
            return validEmail;
        }

        public static void writeIntoFile(Dictionary<string, Record> recordMap, String fileName)
        {
            // write contents of in-memory map into file
            using (TextWriter tw = new StreamWriter(fileName, false))
            {
                foreach (string key in recordMap.Keys)
                {
                    Record record = recordMap[key];
                    tw.WriteLine(record.ToString());
                }
            }
        }

    }
}
