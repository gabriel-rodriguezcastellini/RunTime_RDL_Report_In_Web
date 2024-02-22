using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.PageReportModel;

namespace RunTime_RDL_Report_In_Web.Reports
{
    public class ReportDefinition
    {
        public PageReport rdlReport = new();

        public ReportDefinition()
        {
            rdlReport.Report.PaperOrientation = PaperOrientation.Landscape;
            rdlReport.Report.PageHeight = "8in";
            rdlReport.Report.PageWidth = "11in";

            //Page Header
            PageHeaderFooter headers = new();
            rdlReport.Report.PageHeader = headers;
            headers.Height = ".5in";
            headers.PrintOnFirstPage = true;
            headers.PrintOnLastPage = true;

            //Page Footer
            PageHeaderFooter footer = new();
            rdlReport.Report.PageFooter = footer;
            footer.Height = ".5in";
            footer.PrintOnFirstPage = true;
            footer.PrintOnLastPage = true;

            TextBox pageNumber = new()
            {
                Name = "pNumber",
                Height = ".5cm",
                Width = "1cm",
                Left = "25cm",
                Top = "0cm",
                Value = "=Globals!PageNumber"
            };
            footer.ReportItems.Add(pageNumber);

            TextBox reportTitle = new()
            {
                Name = "Report Title",
                Value = "=\"List of Customers in \" & Parameters!selectedCountry.Value",
                Height = "0.5in",
                Width = "5in",
                Top = "0in",
                Left = "0.5in",
                Style = { TextAlign = "Left", FontSize = "18pt", FontWeight = "Bold" }
            };

            Table customersTable = new()
            {
                Name = "CustomersTable",
                Top = "0.75in",
                Left = "0.5in",
                DataSetName = "MyDataSet"
            };

            #region Table Headers

            //Creating table header
            customersTable.Header = new Header();
            _ = customersTable.Header.TableRows.Add(new TableRow() { Height = ".4in" });
            TableCellCollection customerHeader = customersTable.Header.TableRows[0].TableCells;

            //First cell in the table header
            _ = customerHeader.Add(new TableCell());
            customerHeader[0].ReportItems.Add(new TextBox()
            {
                Name = "Company Name Header",
                Value = "Company Name",
                Style = { BorderStyle = { Bottom = "Solid" },
                    VerticalAlign = "Middle",
                    PaddingLeft="3pt",
                    TextAlign = "Left",
                    BackgroundColor = "WhiteSmoke",
                    FontWeight = "Bold" }
            });
            customersTable.TableColumns.Add(new TableColumn() { Width = "2.35in" });

            //Second cell in the table header
            _ = customerHeader.Add(new TableCell());
            customerHeader[1].ReportItems.Add(new TextBox()
            {
                Name = "Contact Name Header",
                Value = "Contact Name",
                Style = { BorderStyle = { Bottom = "Solid" },
                    VerticalAlign = "Middle",
                    TextAlign = "Left",
                    BackgroundColor = "WhiteSmoke",
                    FontWeight = "Bold" }
            });
            customersTable.TableColumns.Add(new TableColumn() { Width = "1.75in" });

            _ = customerHeader.Add(new TableCell());
            customerHeader[2].ReportItems.Add(new TextBox() { Name = "Address Header", Value = "Address", CanGrow = true, Style = { BorderStyle = { Bottom = "Solid" }, VerticalAlign = "Middle", TextAlign = "Left", BackgroundColor = "WhiteSmoke", FontWeight = "Bold" } });
            customersTable.TableColumns.Add(new TableColumn() { Width = "3in" });

            _ = customerHeader.Add(new TableCell());
            customerHeader[3].ReportItems.Add(new TextBox() { Name = "City Header", Value = "City", Style = { BorderStyle = { Bottom = "Solid" }, VerticalAlign = "Middle", TextAlign = "Left", BackgroundColor = "WhiteSmoke", FontWeight = "Bold" } });
            customersTable.TableColumns.Add(new TableColumn() { Width = "1.5in" });

            _ = customerHeader.Add(new TableCell());
            customerHeader[4].ReportItems.Add(new TextBox() { Name = "Phone Header", Value = "Phone", Style = { BorderStyle = { Bottom = "Solid" }, VerticalAlign = "Middle", TextAlign = "Left", BackgroundColor = "WhiteSmoke", FontWeight = "Bold" } });
            customersTable.TableColumns.Add(new TableColumn() { Width = "1.1in" });

            #endregion

            #region Table Detail Row

            //Detail Row
            customersTable.Details.TableRows.Clear();
            _ = customersTable.Details.TableRows.Add(new TableRow() { Height = ".3in" });
            TableCellCollection customerDetails = customersTable.Details.TableRows[0].TableCells;

            //First cell in the Details row
            _ = customerDetails.Add(new TableCell());
            customerDetails[0].ReportItems.Add(new TextBox()
            {
                Name = "CompanyNameBox",
                Value = "=Fields!CompanyName.Value",
                Width = "2.35in",
                Style = { BorderColor = { Bottom = "WhiteSmoke" }, BorderStyle = { Bottom = "Solid" }, TextAlign = "Left", PaddingLeft = "3pt", VerticalAlign = "Middle" }
            });

            //Second cell in the Details row
            _ = customerDetails.Add(new TableCell());
            customerDetails[1].ReportItems.Add(new TextBox()
            {
                Name = "ContactNameBox",
                Value = "=Fields!ContactName.Value",
                Width = "1.75in",
                Style = { BorderColor = { Bottom = "WhiteSmoke" }, BorderStyle = { Bottom = "Solid" }, TextAlign = "Left", VerticalAlign = "Middle" }
            });

            _ = customerDetails.Add(new TableCell());
            customerDetails[2].ReportItems.Add(new TextBox() { Name = "AddressBox", Value = "=Fields!Address.Value", Width = "3in", CanGrow = true, Style = { BorderColor = { Bottom = "WhiteSmoke" }, BorderStyle = { Bottom = "Solid" }, TextAlign = "Left", VerticalAlign = "Middle" } });

            _ = customerDetails.Add(new TableCell());
            customerDetails[3].ReportItems.Add(new TextBox() { Name = "CityBox", Value = "=Fields!City.Value", Width = "1.5in", Style = { BorderColor = { Bottom = "WhiteSmoke" }, BorderStyle = { Bottom = "Solid" }, TextAlign = "Left", VerticalAlign = "Middle" } });

            _ = customerDetails.Add(new TableCell());
            customerDetails[4].ReportItems.Add(new TextBox() { Name = "PhoneBox", Value = "=Fields!Phone.Value", Width = "1.1in", Style = { BorderColor = { Bottom = "WhiteSmoke" }, BorderStyle = { Bottom = "Solid" }, TextAlign = "Left", VerticalAlign = "Middle" } });
            #endregion

            #region Table Footer

            //Table footer
            customersTable.Footer = new Footer();
            _ = customersTable.Footer.TableRows.Add(new TableRow() { Height = ".5in" });
            TableCellCollection customerFooter = customersTable.Footer.TableRows[0].TableCells;

            //First cell in the footer
            _ = customerFooter.Add(new TableCell());
            customerFooter[0].ReportItems.Add(new TextBox()
            {
                Name = "Company Name Footer",
                Value = "=\"Total Customer Count: \" & CountRows()",
                Style = {
                    VerticalAlign ="Middle",
                    PaddingLeft="3pt",
                    TextAlign = "Left",
                    FontWeight = "Bold" }
            });
            customersTable.TableColumns.Add(new TableColumn() { Width = "2.35in" });

            //Second cell in the footer
            _ = customerFooter.Add(new TableCell());
            customerFooter[1].ReportItems.Add(new TextBox()
            {
                Name = "Contact Name Footer",
                Style = {
                    VerticalAlign = "Middle",
                    TextAlign = "Left",
                    FontWeight = "Bold" }
            });
            customersTable.TableColumns.Add(new TableColumn() { Width = "1.75in" });

            _ = customerFooter.Add(new TableCell());
            customerFooter[2].ReportItems.Add(new TextBox() { Name = "Address Footer", CanGrow = true, Style = { VerticalAlign = "Middle", TextAlign = "Right", FontWeight = "Bold" } });
            customersTable.TableColumns.Add(new TableColumn() { Width = "2in" });

            _ = customerFooter.Add(new TableCell());
            customerFooter[3].ReportItems.Add(new TextBox() { Name = "City Footer", Style = { VerticalAlign = "Middle", TextAlign = "Right", FontWeight = "Bold" } });
            customersTable.TableColumns.Add(new TableColumn() { Width = "1.5in" });

            _ = customerFooter.Add(new TableCell());
            customerFooter[4].ReportItems.Add(new TextBox() { Name = "Phone Footer", Style = { VerticalAlign = "Middle", TextAlign = "Right", FontWeight = "Bold" } });
            customersTable.TableColumns.Add(new TableColumn() { Width = "1.1in" });

            #endregion

            #region Create a parameter 
            //Create a hidden parameter
            ReportParameter selectedCountry = new()
            {
                Name = "selectedCountry",
                Prompt = "Select a country",
                Hidden = true
            };

            #endregion

            rdlReport.Report.ReportParameters.Add(selectedCountry);
            rdlReport.Report.DataSources.Add(MyDataSource());
            rdlReport.Report.DataSets.Add(MyDataSet());

            rdlReport.Report.Body.ReportItems.Add(reportTitle);
            rdlReport.Report.Body.ReportItems.Add(customersTable);

            rdlReport.Run();
        }

        private static DataSource MyDataSource()
        {
            DataSource myDS = new()
            {
                Name = "MyDataSource"
            };
            myDS.ConnectionProperties.DataProvider = "JSON";
            myDS.ConnectionProperties.ConnectString = "jsondoc=https://demodata.grapecity.com/northwind/api/v1/Customers";
            return myDS;
        }

        private static DataSet MyDataSet()
        {
            DataSet myDSet = new();
            Query myQuery = new();
            myDSet.Name = "MyDataSet";
            myQuery.DataSourceName = "MyDataSource";
            myQuery.CommandText = "$.[*]";
            myDSet.Query = myQuery;

            //Create individual fields
            Field country = new("country", "country", null);
            Field compName = new("companyName", "companyName", null);
            Field contactName = new("contactName", "contactName", null);
            Field address = new("address", "address", null);
            Field cityName = new("city", "city", null);
            Field phone = new("phone", "phone", null);

            //Create filter to use Parameter
            Filter countryFilter = new()
            {
                FilterExpression = "=Fields!country.Value",
                FilterValues = { "=Parameters!selectedCountry.Value" }
            };

            //Add fields and filter to the dataset
            myDSet.Fields.Add(country);
            myDSet.Fields.Add(compName);
            myDSet.Fields.Add(contactName);
            myDSet.Fields.Add(address);
            myDSet.Fields.Add(cityName);
            myDSet.Fields.Add(phone);
            myDSet.Filters.Add(countryFilter);

            return myDSet;
        }
    }
}
