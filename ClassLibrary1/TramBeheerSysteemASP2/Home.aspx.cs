using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLibrary;

namespace TramBeheerSysteemASP
{
    public partial class Home : System.Web.UI.Page
    {
        int xPosTb = 5;
        int yPosTb = 5;
        int horizontalRows = 1;
        int verticalRows = 1;
        private int maxSectors = 0;
        private bool tramopspoor = false;
        int TableCnt = 1;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            TextBox1.Text = e.Item.Text;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Sector> sectorLst = new List<Sector>();
            List<Spoor> spoorLst = new List<Spoor>();
            for (int i=1;i<8;i++)
            {
                Sector sctr = new Sector(1, i,null, i, true, false);
                sectorLst.Add(sctr);
            }
            for (int i = 1; i < 44; i++)
            {
                Spoor sp = new Spoor(i, null, i, 8, true, false, sectorLst);
                spoorLst.Add(sp);
            }
            List<Spoor> spoorList = DatabaseManager.LaadSporen();
            AddTextBoxToTable(spoorList);
        }
        // <summary>
        /// Functie om op dynamische wijze textboxen die de sporen en sectoren voorstellen toe te voegen.
        /// </summary>
        /// <param name="SpoorList">Lijst met sporen die toegevoegd worden</param>
        /*
        private void AddTextBoxes(List<Spoor> SpoorList)
        {
            foreach (Spoor sp in SpoorList)
            {
                int tbWidth = 35;
                int tbHeight = 20;
                TextBox spoorTb = new TextBox
                {
                    Text = sp.Nummer.ToString(),
                    Width = tbWidth,
                    Height = tbHeight,
                    ReadOnly = true,
                };
                spoorTb.Style["Top"] = yPosTb.ToString();
                spoorTb.Style["Left"] = xPosTb.ToString();
                PanelTBS.Controls.Add(spoorTb);
                foreach (Sector se in sp.SectorList)
                {
                    TextBox sectorTb = new TextBox
                    {
                        Width = tbWidth,
                        Height = tbHeight
                        //Tag = Convert.ToString(se.Id) + "_" + Convert.ToString(se.SpoorNummer) + "-" + Convert.ToString(se.Nummer)
                    };
                    sectorTb.Attributes.Add("Tag", Convert.ToString(se.Id) + "_" + Convert.ToString(se.SpoorNummer) + "-" + Convert.ToString(se.Nummer));
                    int newTopInt =  yPosTb + (5 * se.Nummer) + (tbHeight * se.Nummer);
                    sectorTb.Style["Top"] = newTopInt.ToString();
                    sectorTb.Style["Left"] = xPosTb.ToString();
                   if ((se.Blokkade || tramopspoor) && se.Tram == null)
                    {
                        sectorTb.Enabled = false;
                        //BlokkeerSporen((sectorTb));
                    }
                    
                    if (se.Tram != null)
                    {
                        sectorTb.Text = se.Tram.nummer.ToString();
                        tramopspoor = true;
                        sectorTb.Enabled = true;
                    }
                    //sectorTb.Click += this.HandleBlockSector;
                    PanelTBS.Controls.Add(sectorTb); 
                }
                tramopspoor = false;
                xPosTb = xPosTb + tbWidth + 5;
                horizontalRows++;
                if (sp.SectorList.Count() > maxSectors)
                {
                    maxSectors = sp.SectorList.Count();
                }
                string strPanelWidth = PanelTBS.Width.ToString();
                strPanelWidth = strPanelWidth.Substring(0,strPanelWidth.IndexOf("px"));
                int PanelWidth;
                Int32.TryParse(strPanelWidth, out PanelWidth);
                if (xPosTb + tbWidth + 25 > PanelWidth)
                {
                    yPosTb = yPosTb + (tbHeight * (maxSectors + 4));
                    xPosTb = 5;
                    verticalRows++;
                    horizontalRows = 1;
                } 
            }
        }*/

        public void AddTextBoxToTable(List<Spoor> spoorList)
        {
            foreach (Spoor sp in spoorList)
            {
                TableRow tSpRow = new TableRow();
                if (TableCnt <=20)
                {
                    TableTrack1.Visible = true;
                    TableTrack1.Rows.Add(tSpRow);
                }
                else if (TableCnt>20 && TableCnt<=40)
                {
                    TableTrack2.Visible = true;
                    TableTrack2.Rows.Add(tSpRow);
                }
                else if (TableCnt>40)
                {
                    TableTrack3.Visible = true;
                    TableTrack3.Rows.Add(tSpRow);
                }
                int tbWidth = 35;
                int tbHeight = 20;
                TextBox spoorTb = new TextBox
                {
                    Text = sp.Nummer.ToString(),
                    Width = tbWidth,
                    Height = tbHeight,
                    ReadOnly = true,
                    Enabled = false
                };
                spoorTb.Style["Top"] = yPosTb.ToString();
                spoorTb.Style["Left"] = xPosTb.ToString();
                    
                    TableCell tSpCell = new TableCell();
                    tSpCell.Controls.Add(spoorTb);
                    tSpRow.Cells.Add(tSpCell);
                
                
                foreach (Sector se in sp.SectorList)
                {
                    TextBox sectorTb = new TextBox
                    {
                        Width = tbWidth,
                        Height = tbHeight
                        //Text = sp.Nummer.ToString() + "-" + se.Nummer.ToString()
                        //Tag = Convert.ToString(se.Id) + "_" + Convert.ToString(se.SpoorNummer) + "-" + Convert.ToString(se.Nummer)
                    };
                    sectorTb.Attributes.Add("Tag", Convert.ToString(se.Id) + "_" + Convert.ToString(se.SpoorNummer) + "-" + Convert.ToString(se.Nummer));
                    TableCell tCell = new TableCell();
                    if((se.Blokkade || tramopspoor)&& se.Tram == null)
                     {
                         sectorTb.Enabled = false;
                         //BlokkeerSporen((sectorTb));
                     }
                     if (se.Tram != null)
                     {
                         sectorTb.Text = se.Tram.nummer.ToString();
                         tramopspoor = true;
                         sectorTb.Enabled = true;
                     }
                     //sectorTb.Click += this.HandleBlockSector;
                    // Create a new cell and add it to the row.
                    tCell.Controls.Add(sectorTb);
                    tSpRow.Cells.Add(tCell);
                }
                //tramopspoor = false;
                TableCnt++;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = maxSectors.ToString();
        }
        
    }
}