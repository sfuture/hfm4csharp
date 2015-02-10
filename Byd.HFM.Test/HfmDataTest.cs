using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Byd.HFM.Test
{
    [TestFixture()]
    public class HfmDataTest
    {
                [SetUp()]
        public void SetUp()
        {

        }

        [TearDown()]
        public void TearDown()
        {
        }

        [Test()]
        public void Test()
        {
            DataTable tesTable =  CreateDataTable();
            var row = tesTable.NewRow();
            row["HFM_SCENARIO"] = "Actual";
            row["HFM_YEAR"] = "2015";
            row["HFM_PERIOD"] = "FM03";
            row["HFM_VIEW"] = "YTD";
            row["HFM_PARENT"] = "WOFE_ONSHORE&Partial_Offshore";
            row["HFM_ENTITY"] = "Songjiang_Onshore";
            row["HFM_VALUE"] = "[Elimination]";
            row["HFM_ACCOUNT"] = "Plug110004_01";
            row["HFM_ICP"] = "[ICP Top]";
            row["HFM_CUSTOM1"] = "TopC1";
            row["HFM_CUSTOM2"] = "Closing";
            row["HFM_CUSTOM3"] = "TopC3";
            row["HFM_CUSTOM4"] = "TopC4";
            tesTable.Rows.Add(row);
            HfmSession hfmSession = HfmHelper.CreateHfmSession();
            HfmData data = hfmSession.GetData();
            data.GetCells(tesTable);
            
            tesTable = CreateDataTable();
            row = tesTable.NewRow();
            row["HFM_SCENARIO"] = "Actual";
            row["HFM_YEAR"] = "2015";
            row["HFM_PERIOD"] = "FM03";
            row["HFM_VIEW"] = "YTD";
            //row["HFM_PARENT"] = "WOFE_ONSHORE&Partial_Offshore";
            row["HFM_ENTITY"] = "52502";
            row["HFM_VALUE"] = "[Proportion]";
            row["HFM_ACCOUNT"] = "20202080134";
            //row["HFM_ICP"] = "[ICP Top]";
            //row["HFM_CUSTOM1"] = "TopC1";
            //row["HFM_CUSTOM2"] = "Closing";
            //row["HFM_CUSTOM3"] = "TopC3";
            //row["HFM_CUSTOM4"] = "TopC4";
            tesTable.Rows.Add(row);
            tesTable.ImportRow(row);
            tesTable.Rows[tesTable.Rows.Count - 1]["HFM_ENTITY"] = "51502";

            var retTable = data.ExpandData(tesTable);



        }

        private DataTable CreateDataTable()
        {
            DataTable tesTable = new DataTable();

            foreach (HfmDimensionType dimensionType in Enum.GetValues(typeof(HfmDimensionType)))
            {
                tesTable.Columns.Add(HfmDimensionTypeHelper.GetDimensionFieldName(dimensionType));
            }
            tesTable.Columns.Add(new DataColumn("VALUE", typeof (double)));
            tesTable.Columns.Add(new DataColumn("STATE", typeof(int)));
            return tesTable;

        }
    }
}
