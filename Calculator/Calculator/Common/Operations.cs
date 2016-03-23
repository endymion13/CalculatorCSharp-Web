using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculator.Common
{
    public class Operations
    {
        //String to inspect
        private string operationString { get; set; }
        private List<string> operationParsed { get; set;}
        private List<string> afterOperationList { get; set; }     
        private double result;
        private string errorMessage;

        //Constructor
        public Operations(string _operation)
        {
            this.operationString = _operation;
            this.result = 0;
            this.operationParsed = new List<string>();
            this.afterOperationList = new List<string>();
        }

        #region Math Operations
        //Addition operation
        private double Addition(double firstNumber, double secondNumber)
        {
            double _result;

            _result = firstNumber + secondNumber;

            return _result;
        }

        //Subtraction operation
        private double Subtraction(double firstNumber, double secondNumber)
        {
            double _result;

            _result = firstNumber - secondNumber;

            return _result;
        }

        //Division operation
        private double Division(double firstNumber, double secondNumber)
        {
            double _result;

            _result = firstNumber / secondNumber;

            return _result;
        }

        //Multiplication operation
        private double Multiplication(double firstNumber, double secondNumber)
        {
            double _result;

            _result = firstNumber * secondNumber;

            return _result;
        }

        //Square root operation
        private double SquareRoot(double value)
        {
            double _result;

            _result = Math.Sqrt(value);

            return _result;
        }

        //Exponent operation
        private double Exponent(double value, double exponentNumber)
        {
            double _result;

            _result = Math.Pow(value, exponentNumber);

            return _result;
        }
        #endregion

        #region Logic
 

        //Parse original operation
        private void parseOperation()
        {
            //Replace command for easy use
            this.operationString = this.operationString.Replace("sqrt(", "{");
            this.operationString = this.operationString.Replace(")", "");

            //Create the operation list
            for (int counter1 = 0; counter1 < this.operationString.Length; counter1++)
            {
                //Construct the node  
                string stringNode =  this.operationString[counter1].ToString();
                //Variable to valid if is a number or symbol
                bool isANumber = false;

                if (stringNode == "-" && IsOperationSymbol(this.operationParsed[operationParsed.Count - 1].ToString()) && counter1 < this.operationString.Length - 1)
                {
                    counter1++;
                    stringNode = stringNode + this.operationString[counter1].ToString();
                }

                //If is a number add all the element of this number(11, 1001, 1.2)
                while (IsNumeric(stringNode) && counter1 < this.operationString.Length-1)
                {
                    counter1++;
                    stringNode = stringNode + this.operationString[counter1].ToString();
                    isANumber = true;
                         
                }

                if (isANumber && counter1 < this.operationString.Length - 1)
                {
                    stringNode = stringNode.Remove(stringNode.Length - 1);
                    counter1--;
                }

               //Add node (symbol or number)
                this.operationParsed.Add(stringNode);
            


            }
               
           

        }

        //Is a Operation valid
        public bool IsOperationSymbol(string _stringExpression)
        {
            bool isOperationSymbol = false;

            switch (_stringExpression)
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                case "{":
                    isOperationSymbol = true;
                break;
            }

            return isOperationSymbol;

        }

        //Valid if is a numeber
        public bool IsNumeric(string stringExpression)
        {
           bool isNum;
           double retNum;
           if (stringExpression[stringExpression.Length - 1].ToString() != "+" && stringExpression[stringExpression.Length - 1].ToString() != "-")
           {
               isNum = Double.TryParse(Convert.ToString(stringExpression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);      
           }
           else
           {
               isNum = false;
           }

           return isNum;

        }

        //Do operation
        private bool do_Operations()
        {
            bool isValidOperation = false;

            try
            {
                parseOperation();

                //Search ^
                for (int cExp = 0; cExp < operationParsed.Count; cExp++)
                {
                    if (this.operationParsed[cExp].ToString() == "^")
                    {
                        double preResult = Exponent(Double.Parse(this.operationParsed[cExp - 1].ToString()), Double.Parse(this.operationParsed[cExp + 1].ToString()));
                        this.afterOperationList[cExp - 1] = preResult.ToString();

                    }
                    else
                    {
                        this.afterOperationList.Add(this.operationParsed[cExp].ToString());
                    }

                }

                //Search sqrt()

                for (int cSqrt = 0; cSqrt < afterOperationList.Count; cSqrt++)
                {
                    if (this.afterOperationList[cSqrt].ToString() == "{")
                    {
                        double preResult = SquareRoot(Double.Parse(this.afterOperationList[cSqrt + 1].ToString()));
                        this.afterOperationList.RemoveAt(cSqrt + 1);
                        this.afterOperationList[cSqrt] = preResult.ToString();
                        cSqrt--;
                    }

                }

                //Search multiplication

                for (int cMult = 0; cMult < afterOperationList.Count; cMult++)
                {
                    if (this.afterOperationList[cMult].ToString() == "*")
                    {
                        double preResult = Multiplication(Double.Parse(this.afterOperationList[cMult - 1].ToString()), Double.Parse(this.afterOperationList[cMult + 1].ToString()));
                        this.afterOperationList.RemoveAt(cMult + 1);
                        this.afterOperationList.RemoveAt(cMult);
                        this.afterOperationList[cMult - 1] = preResult.ToString();
                        cMult--;
                    }

                }

                //Search Division
                for (int cDiv = 0; cDiv < afterOperationList.Count; cDiv++)
                {
                    if (this.afterOperationList[cDiv].ToString() == "/")
                    {
                        double preResult = Division(Double.Parse(this.afterOperationList[cDiv - 1].ToString()), Double.Parse(this.afterOperationList[cDiv + 1].ToString()));
                        this.afterOperationList.RemoveAt(cDiv + 1);
                        this.afterOperationList.RemoveAt(cDiv);
                        this.afterOperationList[cDiv - 1] = preResult.ToString();
                        cDiv--;
                    }

                }

                //Search Addition
                for (int cAddit = 0; cAddit < afterOperationList.Count; cAddit++)
                {

                    if (this.afterOperationList[cAddit].ToString() == "+")
                    {
                        double preResult = Addition(Double.Parse(this.afterOperationList[cAddit - 1].ToString()), Double.Parse(this.afterOperationList[cAddit + 1].ToString()));
                        this.afterOperationList.RemoveAt(cAddit + 1);
                        this.afterOperationList.RemoveAt(cAddit);
                        this.afterOperationList[cAddit - 1] = preResult.ToString();
                        cAddit--;
                    }


                }

                // Search Subtraction
                for (int cSubt = 0; cSubt < afterOperationList.Count; cSubt++)
                {

                    if (this.afterOperationList[cSubt].ToString() == "-")
                    {
                        double preResult = Subtraction(Double.Parse(this.afterOperationList[cSubt - 1].ToString()), Double.Parse(this.afterOperationList[cSubt + 1].ToString()));
                        this.afterOperationList.RemoveAt(cSubt + 1);
                        this.afterOperationList.RemoveAt(cSubt);
                        this.afterOperationList[cSubt - 1] = preResult.ToString();
                        cSubt--;
                    }


                }

                this.result = Double.Parse(this.afterOperationList[0].ToString());
                isValidOperation = true;
            }
            catch(Exception ex)
            {
                Console.Write("An error ocurred:  " + ex.ToString());
                this.errorMessage = "An error ocurred:  " + ex.ToString();
                isValidOperation = false;
            }

            return isValidOperation;
           
        }


        //Return result
        public string get_Result()
        {
            if (do_Operations())
            {
                return this.result.ToString();
            }
            else
            {
                return this.errorMessage;
            }
        }


        #endregion

    }
}