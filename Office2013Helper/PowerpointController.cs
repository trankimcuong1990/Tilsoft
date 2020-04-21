using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Office2013Helper
{
    public class PowerpointController
    {
        public static readonly object Powerpoint2013LockObject = new object();

        public string ExportOffer(DataSet data)
        {
            throw new Exception("Export to power point is underconstruction!");
#pragma warning disable CS0162 // Unreachable code detected
            lock (PowerpointController.Powerpoint2013LockObject)
#pragma warning restore CS0162 // Unreachable code detected
            {
                try
                {
                    DataTable dtOfferPrintoutPP_View = data.Tables["OfferMng_OfferPrintoutPP_View"];
                    DataRow drOfferPrintoutPP_View = dtOfferPrintoutPP_View.Rows[0];

                    DataTable dtOfferPrintoutPP_Detail_View = data.Tables["OfferMng_OfferPrintoutPP_Detail_View"];

                    DataTable dtOfferPrintoutPP_PieceDetail_View = data.Tables["OfferMng_OfferPrintoutPP_PieceDetail_View"];

                    // init power point
                    Microsoft.Office.Interop.PowerPoint.Application app = new Microsoft.Office.Interop.PowerPoint.Application();

                    // Create the presentation ~ hit new button in powerpoint
                    Microsoft.Office.Interop.PowerPoint.Presentation presentation = app.Presentations.Add();

                    //Add Slide 1
                    var slide1 = presentation.Slides.AddSlide(presentation.Slides.Count + 1, presentation.SlideMaster.CustomLayouts[1]);
                    slide1.Layout = Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank;

                    try
                    {
                        //Add logo
                        slide1.Shapes.AddPicture(FrameworkSetting.Setting.MediaUrl + "eurofar-logo.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)37.5, (float)25.5);

                        //Add logo 2
                        slide1.Shapes.AddPicture(FrameworkSetting.Setting.MediaUrl + "thumbnail/" + drOfferPrintoutPP_View["ClientLogoThumbnailLocation"], Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)757.98425, (float)25.51, (float)163.8425, (float)84.47244);

                    }
                    catch (Exception ex)
                    {
                    }

                    string offerClientTemp = (drOfferPrintoutPP_View["OfferUD"].ToString()).Substring(10);
                    string offerClientName = offerClientTemp.Substring(0, offerClientTemp.IndexOf('/'));
                    //Add Text
                    var shape = slide1.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)112.252, (float)143.4331, (float)763.37, (float)118.2047);
                    shape.TextFrame.TextRange.Text = "Offer " + offerClientName;
                    shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                    shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorBottom;
                    shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                    shape.TextFrame.TextRange.Font.Name = "Calibri";
                    shape.TextFrame.TextRange.Font.Size = 44;
                    shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                    shape.TextFrame.HorizontalAnchor = Microsoft.Office.Core.MsoHorizontalAnchor.msoAnchorCenter;
                    shape.TextFrame.TextRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.PowerPoint.PpParagraphAlignment.ppAlignCenter;
                    shape.Width = (float)763.37;
                    shape.Height = (float)118.2047;
                    shape.Left = (float)112.252;
                    shape.Top = (float)143.4331;

                    string season = drOfferPrintoutPP_View["Season"].ToString().Replace("/", "-");

                    shape = slide1.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)119.9055, (float)279.2126, (float)720, (float)77.66929);
                    shape.TextFrame.TextRange.Text = "Season " + season;
                    shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                    shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                    shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                    shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                    shape.TextFrame.TextRange.Font.Name = "Calibri";
                    shape.TextFrame.TextRange.Font.Size = 24;
                    shape.TextFrame.HorizontalAnchor = Microsoft.Office.Core.MsoHorizontalAnchor.msoAnchorCenter;
                    shape.Width = (float)720;
                    shape.Height = (float)77.66929;

                    // add line 
                    shape = slide1.Shapes.AddLine(0, (float)432.85, (float)960.1, (float)432.85);
                    shape.Line.ForeColor.RGB = System.Drawing.Color.FromArgb(64, 160, 64).ToArgb();
                    shape.Line.DashStyle = Microsoft.Office.Core.MsoLineDashStyle.msoLineSolid;
                    shape.Line.Weight = 3;

                    shape = slide1.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)3.685039, (float)443.90551, (float)183.1181, (float)94.3937);
                    shape.TextFrame.TextRange.Text = "Eurofar International B.V.\n" +
                                                     "Beelaerts van Bloklandstraat 14\n" +
                                                     "5042 PM Tilburg\nThe Netherlands\n" +
                                                     "Tel: +31 13 5446193\n" +
                                                     "www.eurofar.com";
                    shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                    shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorMiddle;
                    shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                    shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                    shape.TextFrame.TextRange.Font.Name = "Calibri";
                    shape.TextFrame.TextRange.Font.Size = 12;
                    shape.TextFrame.TextRange.Font.Color.RGB = System.Drawing.Color.FromArgb(22, 132, 22).ToArgb();
                    shape.TextFrame.HorizontalAnchor = Microsoft.Office.Core.MsoHorizontalAnchor.msoAnchorNone;
                    shape.Width = (float)183.1181;
                    shape.Height = (float)94.3937;

                    shape = slide1.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)267.874, (float)444.75591, (float)348.37795, (float)65.48031);
                    shape.TextFrame.TextRange.Text = "Eurofar contactperson:            " + drOfferPrintoutPP_View["SaleNM"] + "\n" +
                                                     "Email:                                          " + drOfferPrintoutPP_View["SaleEmail"] + "\n" +
                                                     "Phone:                                        " + drOfferPrintoutPP_View["SaleTelephone"];
                    shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                    shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                    shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                    shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                    shape.TextFrame.TextRange.Font.Name = "Calibri";
                    shape.TextFrame.TextRange.Font.Size = 12;
                    shape.TextFrame.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                    shape.TextFrame.HorizontalAnchor = Microsoft.Office.Core.MsoHorizontalAnchor.msoAnchorNone;
                    shape.Width = (float)348.37795;
                    shape.Height = (float)65.48031;

                    shape = slide1.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)621.92126, (float)444.75591, (float)318.89764, (float)51.0236);
                    shape.TextFrame.TextRange.Text = "Your contactperson:                " + drOfferPrintoutPP_View["ClientContactPerson"] + "\n" +
                                                     "Email:                                         " + drOfferPrintoutPP_View["ClientContactEmail"] + "\n" +
                                                     "Phone:                                       " + drOfferPrintoutPP_View["ClientContactPhone"];
                    shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                    shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                    shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                    shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                    shape.TextFrame.TextRange.Font.Name = "Calibri";
                    shape.TextFrame.TextRange.Font.Size = 12;
                    shape.TextFrame.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                    shape.TextFrame.HorizontalAnchor = Microsoft.Office.Core.MsoHorizontalAnchor.msoAnchorNone;
                    shape.Width = (float)318.89764;
                    shape.Height = (float)65.48031;

                    //Add Slide 2
                    var slide2 = presentation.Slides.AddSlide(presentation.Slides.Count + 1, presentation.SlideMaster.CustomLayouts[1]);
                    slide2.Layout = Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank;

                    //Add logo
                    slide2.Shapes.AddPicture(FrameworkSetting.Setting.MediaUrl + "eurofar-logo.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)343.84252, (float)31.1811);

                    //Add Text 1
                    shape = slide2.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)127.8425, (float)128.6929, (float)720, (float)112.252);
                    shape.TextFrame.TextRange.Text = "Here with we have the pleasure to send you this customized quotation.\nThe items in this offer are based on your selection and includes the latest\ntrends and developments in the market.";
                    shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                    shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                    shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                    shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                    shape.TextFrame.TextRange.Font.Name = "Calibri Light";
                    shape.TextFrame.TextRange.Font.Size = 24;
                    shape.TextFrame.TextRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.PowerPoint.PpParagraphAlignment.ppAlignCenter;
                    shape.TextFrame.HorizontalAnchor = Microsoft.Office.Core.MsoHorizontalAnchor.msoAnchorCenter;
                    shape.Width = (float)720;
                    shape.Height = (float)112.252;

                    // add line 
                    shape = slide2.Shapes.AddLine(0, (float)255.685, (float)960.1, (float)255.685);
                    shape.Line.ForeColor.RGB = System.Drawing.Color.FromArgb(64, 160, 64).ToArgb();
                    shape.Line.DashStyle = Microsoft.Office.Core.MsoLineDashStyle.msoLineSolid;
                    shape.Line.Weight = 3;

                    string validUntil = "";
                    if (drOfferPrintoutPP_View["ValidUntil"].ToString().Trim() != "")
                    {
                        validUntil = drOfferPrintoutPP_View["ValidUntil"].ToString().Substring(0, 10);
                    }
                    //Add Text 2
                    shape = slide2.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)109.7008, (float)255.685, (float)793.701, (float)253.1339);
                    shape.TextFrame.TextRange.Text = "Our offer is based on:\n\r" +
                                                     drOfferPrintoutPP_View["DeliveryTermNM"] + "\n" +
                                                     "Prices in " + drOfferPrintoutPP_View["Currency"] + "\n" +
                                                     drOfferPrintoutPP_View["PaymentTermNM"] + "\n" +
                                                     drOfferPrintoutPP_View["Remark"] + "\n" +
                                                     "Prices valid until " + validUntil;
                    shape.TextFrame.TextRange.Lines(1).ParagraphFormat.Alignment = Microsoft.Office.Interop.PowerPoint.PpParagraphAlignment.ppAlignCenter;
                    shape.TextFrame.TextRange.Paragraphs(2).ParagraphFormat.Bullet.Type = Microsoft.Office.Interop.PowerPoint.PpBulletType.ppBulletUnnumbered;
                    shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                    shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                    shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                    shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                    shape.TextFrame.TextRange.Font.Name = "Calibri Light";
                    shape.TextFrame.TextRange.Font.Size = 24;
                    shape.Width = (float)720;
                    shape.Height = (float)253.1339;

                    foreach (DataRow dr in dtOfferPrintoutPP_Detail_View.Rows)
                    {
                        //Add Slide 3
                        var slide3 = presentation.Slides.AddSlide(presentation.Slides.Count + 1, presentation.SlideMaster.CustomLayouts[1]);
                        slide3.Layout = Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank;

                        try
                        {
                            // add large picture
                            shape = slide3.Shapes.AddPicture(FrameworkSetting.Setting.MediaUrl + "file/" + dr["GardenImageLocation"].ToString(), Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 0, 0);
                            shape.Width = (float)962.07874;
                            shape.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoFalse;
                            shape.Height = (float)543.9685;
                        }
                        catch (Exception ex)
                        {

                        }


                        //Add Text 1
                        shape = slide3.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)401.95276, (float)464.8819, (float)448.15748, (float)84.47244);
                        shape.TextFrame.TextRange.Text = dr["ModelNM"].ToString();
                        shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                        shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorMiddle;
                        shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                        shape.TextFrame.TextRange.Font.Name = "Calibri";
                        shape.TextFrame.TextRange.Font.Size = 44;
                        shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                        shape.TextFrame.TextRange.Font.Color.RGB = System.Drawing.Color.FromArgb(255, 255, 255).ToArgb();
                        shape.Width = (float)448.15748;
                        shape.Height = (float)84.47244;
                        shape.Left = (float)401.95276;
                        shape.Top = (float)464.8819;

                        shape = slide3.Shapes.AddPicture(FrameworkSetting.Setting.MediaUrl + "offer-print-pp-image-1.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)767.90551, (float)480.18898);
                        shape.Width = (float)181.9843;
                        shape.Height = (float)49.6063;

                        //Add Slide 4
                        var slide4 = presentation.Slides.AddSlide(presentation.Slides.Count + 1, presentation.SlideMaster.CustomLayouts[1]);
                        slide4.Layout = Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutBlank;

                        string productName = dr["ModelNM"].ToString();
                        if (int.Parse(dr["ProductTypeID"].ToString()) == 5)
                        {
                            productName = productName.Substring(0, productName.IndexOf("(")) + productName.Substring(productName.LastIndexOf(")") + 1);
                        }

                        //Add Text 1
                        shape = slide4.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)13.6063, (float)14.45669, (float)828, (float)109.4173);
                        shape.TextFrame.TextRange.Text = productName + "\r" +
                                                        "Reference: " + dr["ModelUD"];
                        shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                        shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorMiddle;
                        shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                        shape.TextFrame.TextRange.Font.Name = "Calibri";
                        shape.TextFrame.TextRange.Font.Size = 44;
                        shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                        shape.TextFrame.TextRange.Paragraphs(1).Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                        shape.TextFrame.TextRange.Paragraphs(2).Font.Size = 16;
                        shape.Width = (float)448.15748;
                        shape.Height = (float)84.47244;
                        shape.Left = (float)13.6063;
                        shape.Top = (float)14.45669;

                        shape = slide4.Shapes.AddPicture(FrameworkSetting.Setting.MediaUrl + "offer-print-pp-image-1.png", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)733.32283, (float)5.102362);
                        shape.Width = (float)221.9528;
                        shape.Height = (float)60.37795;

                        string freescanImageLocation = dr["FreescanImageLocation"].ToString();

                        try
                        {
                            // add large picture
                            shape = slide4.Shapes.AddPicture(FrameworkSetting.Setting.MediaUrl + "thumbnail/" + freescanImageLocation, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, (float)23.81102, (float)112.8189);
                            shape.Width = (float)409.03937;
                            shape.Height = (float)176.8819;
                        }
                        catch (Exception ex)
                        {

                        }

                        //Add text 2
                        string currentcy = "";
                        if (drOfferPrintoutPP_View["Currency"].ToString() == "USD")
                        {
                            currentcy = "$ ";
                        }
                        else
                        {
                            if (drOfferPrintoutPP_View["Currency"].ToString() == "EUR")
                            {
                                currentcy = "€ ";
                            }
                        }
                        shape = slide4.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)598.1102, (float)185.6693, (float)334.20472, (float)32.88189);
                        shape.TextFrame.TextRange.Text = "Unit Price\t" + currentcy + float.Parse(dr["FinalPrice"].ToString()).ToString("n2");
                        shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                        shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                        shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                        shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                        shape.TextFrame.TextRange.Font.Name = "Calibri";
                        shape.TextFrame.TextRange.Font.Size = 24;
                        shape.TextFrame.TextRange.Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                        shape.TextFrame.HorizontalAnchor = Microsoft.Office.Core.MsoHorizontalAnchor.msoAnchorNone;
                        shape.Width = (float)334.20472;
                        shape.Height = (float)32.88189;

                        //Add text 3
                        string tab1 = "", tab2 = "", tab3 = "", tab4 = "";
                        if (dr["MaterialNM"].ToString().Length <= 18 && dr["MaterialNM"].ToString().Length > 8)
                        {
                            tab1 = "\t";
                        }
                        else if (dr["MaterialNM"].ToString().Length <= 8)
                        {
                            tab1 = "\t\t";
                        }

                        if (dr["SubMaterialNM"].ToString().Length <= 18 && dr["SubMaterialNM"].ToString().Length > 8)
                        {
                            tab2 = "\t";
                        }
                        else if (dr["SubMaterialNM"].ToString().Length <= 8)
                        {
                            tab2 = "\t\t";
                        }

                        if (dr["FrameMaterialNM"].ToString().Length <= 18 && dr["FrameMaterialNM"].ToString().Length > 8)
                        {
                            tab3 = "\t";
                        }
                        else if (dr["FrameMaterialNM"].ToString().Length <= 8)
                        {
                            tab3 = "\t\t";
                        }

                        if ((dr["SeatCushionNM"].ToString().Length + 13) <= 18)
                        {
                            tab4 = "\t";
                        }
                        shape = slide4.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)23.81102, (float)295.65354, (float)908.50394, (float)105.7323);
                        shape.TextFrame.TextRange.Text = "Material 1: \t" + dr["MaterialNM"] + tab1 + "\tColor:\t" + dr["MaterialColorNM"] + "\n" +
                                                         "Material 2: \t" + dr["SubMaterialNM"] + tab2 + "\tColor:\t" + dr["SubMaterialColorNM"] + "\n" +
                                                         "Frame:\t\t" + dr["FrameMaterialNM"] + tab3 + "\tColor:\t" + dr["FrameMaterialColorNM"] + "\n" +
                                                         "Cushion:\t\tSeatcushion: " + dr["SeatCushionNM"] + tab4 + "\tColor:\t" + dr["CushionColorNM"] + "\n" +
                                                         "\t\tBackcushion: " + dr["BackCushionNM"];
                        shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                        shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                        shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                        shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                        shape.TextFrame.TextRange.Font.Name = "Calibri";
                        shape.TextFrame.TextRange.Font.Size = 16;
                        shape.Width = (float)908.50394;
                        shape.Height = (float)105.7323;

                        // add line 
                        shape = slide4.Shapes.AddLine(0, (float)406.77165, (float)960.1, (float)406.77165);
                        shape.Line.ForeColor.RGB = System.Drawing.Color.FromArgb(64, 160, 64).ToArgb();
                        shape.Line.DashStyle = Microsoft.Office.Core.MsoLineDashStyle.msoLineSolid;
                        shape.Line.Weight = 3;

                        if (int.Parse(dr["ProductTypeID"].ToString()) == 5)
                        {
                            string txtDetail = "";
                            string txtPackaging = "";
                            int boxNumber = 1;
                            string tab = "";

                            foreach (DataRow drDetail in dtOfferPrintoutPP_PieceDetail_View.Rows)
                            {
                                string modelNameTemp = drDetail["ModelNM"].ToString();
                                string modelName = modelNameTemp.Substring(modelNameTemp.IndexOf(" "));
                                if (modelName.Length > 18)
                                {
                                    modelName = modelName.Substring(0, 15) + "...";
                                }
                                if ((modelName.Substring(modelName.IndexOf(" ")).Length + 6) < 16)
                                {
                                    tab = "\t";
                                }
                                else
                                {
                                    tab = "";
                                }

                                if (drDetail["ModelID"].ToString() == dr["ModelID"].ToString() && drDetail["PackagingMethodID"].ToString() == dr["PackagingMethodID"].ToString())
                                {
                                    txtDetail += drDetail["Quantity"] + " x" + modelName + ": " + tab + "\tL: " + drDetail["OverallDimL"] + " x W: " + drDetail["OverallDimW"] + " x H: " + drDetail["OverallDimH"] + " CM\t\tQnt/40’HC: \t" + drDetail["Qnt40HC"] + "\n";
                                    txtPackaging += "Box " + boxNumber + ":\tL: " + drDetail["CartonBoxDimL"] + " x W: " + drDetail["CartonBoxDimW"] + " x H: " + drDetail["CartonBoxDimH"] + " CM\n";
                                    boxNumber++;
                                }
                            }
                            //Add text 4
                            shape = slide4.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)23.81102, (float)419.81102, (float)514.20472, (float)113.6693);
                            shape.TextFrame.TextRange.Text = "Specifications and loading:\n" + txtDetail + "\r" +
                                                             "\t\t\t\t\tSets/40’HC:\t" + dr["Qnt40HC"];
                            shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                            shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                            shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                            shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                            shape.TextFrame.TextRange.Font.Name = "Calibri";
                            shape.TextFrame.TextRange.Lines(1).Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                            shape.TextFrame.TextRange.Paragraphs(2).Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                            //shape.TextFrame.TextRange.Lines(10).Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                            shape.TextFrame.TextRange.Font.Size = 13;
                            shape.Width = (float)514.20472;
                            shape.Height = (float)113.6693;

                            //Add text 5
                            shape = slide4.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)598.1102, (float)426.89764, (float)346.67717, (float)96.09449);
                            shape.TextFrame.TextRange.Text = "Packaging:\tStandard with carton box\n\n" + txtPackaging;
                            shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                            shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                            shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                            shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                            shape.TextFrame.TextRange.Font.Name = "Calibri";
                            shape.TextFrame.TextRange.Font.Size = 13;
                            shape.TextFrame.TextRange.Words(1).Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                            shape.Width = (float)346.67717;
                            shape.Height = (float)96.09449;
                        }
                        else
                        {
                            //Add text 4
                            string modelName = dr["ModelNM"].ToString();
                            if (modelName.Length > 18)
                            {
                                modelName = modelName.Substring(0, 15) + "...";
                            }
                            string tab = "";
                            if (("L: " + dr["OverallDimL"] + " x W: " + dr["OverallDimW"] + " x H: " + dr["OverallDimH"] + " CM").Length < 26)
                            {
                                tab = "\t";
                            }
                            shape = slide4.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)23.81102, (float)419.81102, (float)514.20472, (float)113.6693);
                            shape.TextFrame.TextRange.Text = "Specifications and loading:\n" +
                                                             "1 x " + modelName + ":\t L: " + dr["OverallDimL"] + " x W: " + dr["OverallDimW"] + " x H: " + dr["OverallDimH"] + " CM" + tab + "\tQnt/40’HC: 	" + dr["Qnt40HC"];
                            shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                            shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                            shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                            shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                            shape.TextFrame.TextRange.Font.Name = "Calibri";
                            shape.TextFrame.TextRange.Font.Size = 13;
                            shape.TextFrame.TextRange.Lines(1).Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                            shape.TextFrame.TextRange.Lines(7).Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                            shape.Width = (float)514.20472;
                            shape.Height = (float)113.6693;

                            //Add text 5
                            shape = slide4.Shapes.AddTextbox(Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal, (float)598.1102, (float)426.89764, (float)346.67717, (float)96.09449);
                            shape.TextFrame.TextRange.Text = "Packaging:\tStandard with carton box\n\n" +
                                                             "Box 1:\tL: " + dr["CartonBoxDimL"] + " x W: " + dr["CartonBoxDimW"] + " x H: " + dr["CartonBoxDimH"] + " CM";
                            shape.TextFrame.Orientation = Microsoft.Office.Core.MsoTextOrientation.msoTextOrientationHorizontal;
                            shape.TextFrame.VerticalAnchor = Microsoft.Office.Core.MsoVerticalAnchor.msoAnchorTop;
                            shape.TextFrame.WordWrap = Microsoft.Office.Core.MsoTriState.msoTrue;
                            shape.TextFrame2.AutoSize = Microsoft.Office.Core.MsoAutoSize.msoAutoSizeTextToFitShape;
                            shape.TextFrame.TextRange.Font.Name = "Calibri";
                            shape.TextFrame.TextRange.Font.Size = 13;
                            shape.TextFrame.TextRange.Words(1).Font.Bold = Microsoft.Office.Core.MsoTriState.msoCTrue;
                            shape.Width = (float)346.67717;
                            shape.Height = (float)96.09449;
                        }
                    }

                    // save file and close powerpoint
                    string filename = System.Guid.NewGuid().ToString().Replace("-", "") + ".pptx";
                    string fullpath = FrameworkSetting.Setting.AbsoluteReportTmpFolder + filename;
                    presentation.SaveAs(fullpath, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault, Microsoft.Office.Core.MsoTriState.msoTrue);
                    presentation.Close();
                    app.Quit();
                    return filename;
                }
                catch
                {
                }
            }

            return string.Empty;
        }
    }
}
