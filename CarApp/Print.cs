using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using CarApp.Data;
using CarApp.Models;
using System.Drawing.Printing;
using System.Drawing;

namespace CarApp
{
    public class Print 
    {
        private static int count = 1;
        private Font printFont;
        private readonly DataContext dataContext;

        public Print(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            List<Car> cars = dataContext.Car.ToList();

            float linesPerPage = ev.MarginBounds.Height /
               printFont.GetHeight(ev.Graphics);

            string titleLine = "List of Cars";

            float titlePosX = ev.MarginBounds.Left + (ev.MarginBounds.Width / 2) - (5*titleLine.Length);
            ev.Graphics.DrawString(titleLine, printFont, Brushes.Black, titlePosX, topMargin, new StringFormat());
            foreach (var car in cars)
            {   
            if (count > linesPerPage)
                {
                    ev.HasMorePages = true;
                    return;
                }
                
                string line = car.CarId + "  |  " + car.RegistrationNumber + "  |  " + car.VIN + "  |  " + car.Model + "  |  " + car.Brand;

                float yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                count++;
            }

            ev.HasMorePages = false;

        }
        public void StartPrint()
        {
            try
            {
                printFont = new Font("Arial", 10);
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}