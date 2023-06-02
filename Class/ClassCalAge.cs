using System;
using System.Collections.Generic;
using System.Text;


    public class clsCalAge
    {
        private DateTime fromDate;
        private DateTime toDate;
        private int intyear;
        private int intmonth;
        private int intday;
        public int Years
        {
            get { return intyear; }
        }

        public int Months
        {
            get { return intmonth; }
        }

        public int Days
        {
            get { return intday; }
        }

        public clsCalAge( DateTime d2)
        {
            //CheckDay
            try
            {

            
            DateTime d1 = DateTime.Now;
            int intCheckedDay = 0;
            if (d1 > d2)
            {
                this.fromDate = d2;
                this.toDate = d1;
            }
            else
            {
                this.fromDate = d1;
                this.toDate = d2;
            }

            intCheckedDay = 0;

            try
            {
                if (this.fromDate.Day > this.toDate.Day)
                {
                    intCheckedDay = System.DateTime.DaysInMonth(this.fromDate.Year, this.fromDate.Month - 1);
                }
            }
            catch (Exception)
            {
                if (this.fromDate.Day > this.toDate.Day)
                {
                    intCheckedDay = System.DateTime.DaysInMonth(this.fromDate.Year, this.fromDate.Month);
                }
                
            }
            

            if (intCheckedDay != 0)
            {
                intday = (this.toDate.Day + intCheckedDay) - this.fromDate.Day;
                intCheckedDay = 1;
            }
            else
            {
                intday = this.toDate.Day - this.fromDate.Day;
            }

            if ((this.fromDate.Month + intCheckedDay) > this.toDate.Month)
            {
                intmonth = (this.toDate.Month + 12) - (this.fromDate.Month + intCheckedDay);
                intCheckedDay = 1;
            }
            else
            {
                intmonth = (this.toDate.Month) - (this.fromDate.Month + intCheckedDay);
                intCheckedDay = 0;
            }
            intyear = this.toDate.Year - (this.fromDate.Year + intCheckedDay);
        }
        catch (Exception)
        {

            return;
        }
        }

        public override string ToString()
        {
            return ((intyear + " ปี  ") + intmonth + " เดือน  ") + intday + " วัน";
        }
    }

