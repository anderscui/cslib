using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Drawing;

namespace Andersc.CodeLib.Common
{
    [Themeable(true)]
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CustomGridView runat='server'></{0}:CustomGridView>"),
    DisplayName("CustomGridView"),
    Description("A custom gridView control.")]
    public class CustomGridView : GridView
    {
        public enum PagerStyleType
        {
            /// <summary>
            /// Using gridview built-in pager style.
            /// </summary>
            Default,
            /// <summary>
            /// Using custom style pager style.
            /// </summary>
            CustomNumeric,
            /// <summary>
            /// TODO: Using dropdownlist pager style.
            /// </summary>
            DropDownList
        }

        private const string ViewStateName = "PagerStyleType";
        private const string ViewStateNameOfTotalPageInfo = "ShowTotalPageInfo";
        private const string ViewStateNameOfShowAllButton = "ShowAllButton";
        private const string ViewStateNameOfHasShowAllItems = "HasShowAllItems";
        private const string ViewStateNameOfShowAllItemCount = "ShowAllItemCount";
        private const string ViewStateNameOfOriginalItemCount = "OriginalItemCount";
        private const string buttonImageFormat = "<img src='{0}' border='0' />";
        //private const string text = "page {0} of {1}, {2} per page, total {3} ";
        private const string totalInfoFormat = "Page {0} of {1}, {2} items per page.  ";

        private const string showAllButtonTextFormat = "Show All";
        private const string showOriginalButtonTextFormat = "Show {0} per page";

        private int recordCount;
        private string mouseOverColor;
        private string pagerButtonCssClass;

        public int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        [Category("Paging")]
        public int OriginalItemCount
        {
            get
            {
                object count = ViewState[ViewStateNameOfOriginalItemCount];
                //if (count == null)
                //{
                //    return this.PageSize;
                //}
                return Convert.ToInt32(count);
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("OriginalItemCount must be a positive number.");
                }
                ViewState[ViewStateNameOfOriginalItemCount] = value;
            }
        }

        [Category("Paging")]
        [DefaultValue(100)]
        public int ShowAllItemCount
        {
            get 
            {
                object count = ViewState[ViewStateNameOfShowAllItemCount];
                if (count == null)
                {
                    return 100;
                }
                return Convert.ToInt32(count);
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("ShowAllItemCount must be a positive number.");
                }
                ViewState[ViewStateNameOfShowAllItemCount] = value;
            }
        }

        [Category("Paging")]
        [DefaultValue("CustomNumeric")]
        public PagerStyleType PagerType
        {
            get
            {
                return (ViewState[ViewStateName] != null ? (PagerStyleType)ViewState[ViewStateName] : PagerStyleType.CustomNumeric);
            }
            set
            {
                ViewState[ViewStateName] = value;
            }
        }

        [Category("Paging")]
        [DefaultValue("false")]
        public bool ShowTotalPageInfo
        {
            get
            {
                return (ViewState[ViewStateNameOfTotalPageInfo] != null ? (bool)ViewState[ViewStateNameOfTotalPageInfo] : false);
            }
            set
            {
                ViewState[ViewStateNameOfTotalPageInfo] = value;
            }
        }

        [Category("Paging")]
        [DefaultValue("false")]
        public bool ShowAllButton
        {
            get
            {
                return (ViewState[ViewStateNameOfShowAllButton] != null ? (bool)ViewState[ViewStateNameOfShowAllButton] : false);
            }
            set
            {
                ViewState[ViewStateNameOfShowAllButton] = value;
            }
        }

        [DefaultValue("")]
        public string MouseOverColor
        {
            get { return mouseOverColor; }
            set { mouseOverColor = value; }
        }

        [Category("Paging")]
        [DefaultValue("")]
        public string PagerButtonCssClass
        {
            get { return pagerButtonCssClass; }
            set { pagerButtonCssClass = value; }
        }

        [Category("Paging")]
        [DefaultValue("false")]
        public bool HasShowAllItems
        {
            get
            {
                return (ViewState[ViewStateNameOfHasShowAllItems] != null ? (bool)ViewState[ViewStateNameOfHasShowAllItems] : false);
            }
            set
            {
                ViewState[ViewStateNameOfHasShowAllItems] = value;
            }
        }

        #region Sorting Image

        private string ascImageUrl;

        public string AscImageUrl
        {
            get { return ascImageUrl; }
            set { ascImageUrl = value; }
        }

        private string descImageUrl;

        public string DescImageUrl
        {
            get { return descImageUrl; }
            set { descImageUrl = value; }
        }

        #endregion

        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            switch (PagerType)
            {
                case PagerStyleType.CustomNumeric:
                    InitializeNumericPager(row, columnSpan, pagedDataSource);
                    break;
                case PagerStyleType.DropDownList:
                    InitializeDropDownListPager(row, columnSpan, pagedDataSource);
                    break;
                default:
                    InitializeDefaultPager(row, columnSpan, pagedDataSource);
                    break;
            }

            if (pagedDataSource != null)
            {
                RecordCount = pagedDataSource.DataSourceCount;
            }
        }

        private void InitializeDefaultPager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            // if PagerType is default, render the regular GridView Pager
            base.InitializePager(row, columnSpan, pagedDataSource);
        }

        private void InitializeNumericPager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            // create a Table that will replace entirely our GridView's Pager section            
            Table tbl = new Table();
            tbl.BorderWidth = 0;
            tbl.Width = Unit.Percentage(100);
            // add one TableRow to our Table
            tbl.Rows.Add(new TableRow());

            // add our first TableCell which will contain the total pager info(total page count, current page index, page size...)
            TableCell cell_1 = new TableCell();

            // we just add a Label with total pager info text.
            if (ShowTotalPageInfo)
            {
                cell_1.Controls.Add(GetTotalPageInfo(pagedDataSource.DataSourceCount));
            }

            // the 2nd TableCell will display the Record number you are currently in.
            TableCell cell_2 = new TableCell();

            // cell_2.Controls.Add(PageInfo(pagedDataSource.DataSourceCount));
            FillPagerButtons(cell_2);

            // add now the 2 cell to our created row
            tbl.Rows[0].Cells.Add(cell_1);
            tbl.Rows[0].Cells.Add(cell_2);
            tbl.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Left;
            tbl.Rows[0].Cells[1].HorizontalAlign = HorizontalAlign.Right;

            // in Pager's Row of our GridView add a TableCell                 
            row.Controls.AddAt(0, new TableCell());
            // sets it span to GridView's number of columns
            row.Cells[0].ColumnSpan = Columns.Count;
            // finally add our created Table
            row.Cells[0].Controls.AddAt(0, tbl);
        }

        private void InitializeDropDownListPager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            // our Pager with DropDownList control

            // create our DropDownList control
            DropDownList ddl = new DropDownList();
            // populate it with the number of Pages of our GridView
            for (int i = 0; i < PageCount; i++)
            {
                ddl.Items.Add(new ListItem(Convert.ToString(i + 1), i.ToString()));
            }
            ddl.AutoPostBack = true;
            // assign an Event Handler when its Selected Index Changed
            ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);
            // synchronize its selected index to GridView's current PageIndex
            ddl.SelectedIndex = PageIndex;

            // create a Table that will replace entirely our GridView's Pager section            
            Table tbl = new Table();
            tbl.BorderWidth = 0;
            tbl.Width = Unit.Percentage(100);
            // add one TableRow to our Table
            tbl.Rows.Add(new TableRow());

            // add our first TableCell which will contain the DropDownList 
            TableCell cell_1 = new TableCell();

            // we just add a Label with 'Page ' Text
            cell_1.Controls.Add(PageOf());

            // our DropDownList control here.
            cell_1.Controls.Add(ddl);

            // and our Total number of Pages
            cell_1.Controls.Add(PageTotal());

            // the 2nd TableCell will display the Record number you are currently in.
            TableCell cell_2 = new TableCell();
            cell_2.Controls.Add(PageInfo(pagedDataSource.DataSourceCount));

            // add now the 2 cell to our created row
            tbl.Rows[0].Cells.Add(cell_1);
            tbl.Rows[0].Cells.Add(cell_2);
            tbl.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Left;
            tbl.Rows[0].Cells[1].HorizontalAlign = HorizontalAlign.Right;

            // in Pager's Row of our GridView add a TableCell                 
            row.Controls.AddAt(0, new TableCell());
            // sets it span to GridView's number of columns
            row.Cells[0].ColumnSpan = Columns.Count;
            // finally add our created Table
            row.Cells[0].Controls.AddAt(0, tbl);
        }

        #region CustomNumeric Style

        private Label GetTotalPageInfo(int dataSourceCount)
        {
            // it is just a label
            Label lbl = new Label();
            lbl.Text = string.Format(totalInfoFormat, this.PageIndex + 1, this.PageCount, this.PageSize, dataSourceCount);
            return lbl;
        }

        private void SetPagerButtonCssClass(LinkButton link)
        {
            if (!string.IsNullOrEmpty(pagerButtonCssClass))
            {
                link.CssClass = pagerButtonCssClass;
            }
        }

        private void AddShowAllButton(TableCell container)
        {
            if (ShowAllButton)
            {
                LinkButton showAll = new LinkButton();
                showAll.Click += new EventHandler(showAll_Click);

                SetShowAllButtonText(showAll);
                
                showAll.Visible = (PageCount > 1);
                SetPagerButtonCssClass(showAll);
                container.Controls.Add(showAll);
                container.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            }
        }

        private void SetShowAllButtonText(LinkButton showAll)
        {
            if (!HasShowAllItems)
            {
                showAll.Text = showAllButtonTextFormat;
            }
            else
            {
                showAll.Text = string.Format(showOriginalButtonTextFormat, OriginalItemCount);
            }
        }

        private void showAll_Click(object sender, EventArgs e)
        {
            HasShowAllItems = !HasShowAllItems;

            if (HasShowAllItems)
            {
                this.PageSize = ShowAllItemCount;
            }
            else
            {
                this.PageSize = OriginalItemCount;
            }

            SetShowAllButtonText(sender as LinkButton);

            this.PageIndex = 0;
            this.DataBind();
        }

        private void AddLastButton(TableCell container)
        {
            LinkButton last = new LinkButton();
            if (!string.IsNullOrEmpty(PagerSettings.LastPageImageUrl))
            {
                last.Text = GetButtonImageString(PagerSettings.LastPageImageUrl);
            }
            else
            {
                last.Text = PagerSettings.LastPageText;
            }
            last.CommandName = "Page";
            last.CommandArgument = "Last";
            SetPagerButtonCssClass(last);
            last.Visible = (PageIndex != PageCount - 1);
            container.Controls.Add(last);
            container.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
        }

        private void AddNextButton(TableCell container)
        {
            LinkButton next = new LinkButton();
            if (!string.IsNullOrEmpty(PagerSettings.NextPageImageUrl))
            {
                next.Text = GetButtonImageString(PagerSettings.NextPageImageUrl);
            }
            else
            {
                next.Text = PagerSettings.NextPageText;
            }
            next.CommandName = "Page";
            next.CommandArgument = "Next";
            SetPagerButtonCssClass(next);
            next.Visible = (PageIndex != PageCount - 1);
            container.Controls.Add(next);
            container.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
        }

        private void AddPrevButton(TableCell container)
        {
            LinkButton prev = new LinkButton();
            if (!string.IsNullOrEmpty(PagerSettings.PreviousPageImageUrl))
            {
                prev.Text = GetButtonImageString(PagerSettings.PreviousPageImageUrl);
            }
            else
            {
                prev.Text = PagerSettings.PreviousPageText;
            }
            prev.CommandName = "Page";
            prev.CommandArgument = "Prev";
            SetPagerButtonCssClass(prev);
            prev.Visible = (PageIndex != 0);
            container.Controls.Add(prev);
            container.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
        }

        private void AddFirstButton(TableCell container)
        {
            LinkButton first = new LinkButton();
            if (!string.IsNullOrEmpty(PagerSettings.FirstPageImageUrl))
            {
                first.Text = GetButtonImageString(PagerSettings.FirstPageImageUrl);
            }
            else
            {
                first.Text = PagerSettings.FirstPageText;
            }
            first.CommandName = "Page";
            first.CommandArgument = "First";
            SetPagerButtonCssClass(first);
            first.Visible = (PageIndex != 0);
            container.Controls.Add(first);
            container.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
        }

        private string GetButtonImageString(string originalUrl)
        {
            return string.Format(buttonImageFormat, ResolveUrl(originalUrl));
        }

        private void FillPagerButtons(TableCell container)
        {
            AddShowAllButton(container);

            // add first and prev buttons
            AddFirstButton(container);
            AddPrevButton(container);

            // if pageButtonCount is larger than pageCount, we should assign pageCount to buttonCount.
            int pageButtonCount = this.PagerSettings.PageButtonCount;
            int skipedButtons = (this.PageIndex / pageButtonCount) * pageButtonCount;
            int buttonCount = Math.Min(pageButtonCount, this.PageCount - skipedButtons);
            // draw numeric buttons
            for (int i = 0; i < buttonCount; i++)
            {
                container.Controls.Add(GetPagerButton(skipedButtons + i, this.PageIndex == (skipedButtons + i)));
                container.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
            }

            // add next and last buttons
            AddNextButton(container);
            AddLastButton(container);
        }

        private Control GetPagerButton(int index, bool isCurrentIndex)
        {
            if (isCurrentIndex)
            {
                return GetLiteral(index);
            }

            LinkButton link = new LinkButton();
            link.Text = (index + 1).ToString();
            link.CommandName = "Page";
            link.CommandArgument = (index + 1).ToString();
            SetPagerButtonCssClass(link);
            return link;
        }

        private LiteralControl GetLiteral(int pageIndex)
        {
            return new LiteralControl(string.Format("<span class='{0}'>{1}</span>", pagerButtonCssClass, (pageIndex + 1).ToString()));
        }

        #endregion

        #region DropDownList Pager Style

        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // on our DropDownList SelectedIndexChanged event call the GridView's OnPageIndexChanging method to raised the PageIndexChanging event.
            // pass the DropDownList SelectedIndex as its argument.
            if (string.IsNullOrEmpty(DataSourceID))
            {
                OnPageIndexChanging(new GridViewPageEventArgs((sender as DropDownList).SelectedIndex));
            }
            else
            {
                // if using DataSourceControl.
                PageIndex = (sender as DropDownList).SelectedIndex;
            }
        }

        protected Label PageOf()
        {
            // it is just a label
            Label lbl = new Label();
            lbl.Text = "Page ";
            return lbl;
        }
        protected Label PageTotal()
        {
            // a label of GridView's Page Count
            Label lbl = new Label();
            lbl.Text = string.Format(" of {0}", PageCount);
            return lbl;
        }
        protected Label PageInfo(int rowCount)
        {
            // create a label that will display the current Record you're in
            Label label = new Label();
            int currentPageFirstRow = ((PageIndex * PageSize) + 1);
            int currentPageLastRow = 0;
            int lastPageRemainder = rowCount % PageSize;
            currentPageLastRow = (PageCount == PageIndex + 1) ? (currentPageFirstRow + lastPageRemainder - 1) : (currentPageFirstRow + PageSize - 1);
            label.Text = String.Format("Record {0} to {1} of {2}", currentPageFirstRow, currentPageLastRow, rowCount);
            return label;
        }

        #endregion

        /// <summary>
        /// Raises the <see cref="GridView.RowCreated"></see> event.
        /// Adds sort status icon.
        /// </summary>
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.Header)
            {
                if (!string.IsNullOrEmpty(ascImageUrl) && !string.IsNullOrEmpty(descImageUrl))
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (cell.HasControls())
                        {
                            LinkButton button = cell.Controls[0] as LinkButton;

                            if (button != null)
                            {
                                if (SortExpression == button.CommandArgument)
                                {
                                    System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();

                                    if (SortDirection == SortDirection.Ascending)
                                        image.ImageUrl = ascImageUrl;
                                    else
                                        image.ImageUrl = descImageUrl;

                                    cell.Controls.Add(image);
                                }
                            }
                        }
                    }
                }
            }

            base.OnRowCreated(e);
        }

        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);
        }
    }
}
