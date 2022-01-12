using CodeBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataConnectionBase
{

    ///<summary></summary>
    public class UserQueries
    {
        ///<summary>List of commands that modify a database. Typically used to parse out modification queries for MassEmail user queries.</summary>
        public static List<string> ListMassEmailBlacklistCmds = new List<string> {
            "INSERT",
            "DELETE",
            "ALTER",
            "DROP",
            "ADD",
            "BACKUP",
            "COLUMN",
            "CREATE",
            "SET",
            "UPDATE",
            "TRUNCATE",
        };
        ///<summary>Returns the list of variables in the query contained within the passed-in SET statement.
        ///Pass in one SET statement. Used in conjunction with GetListVals.</summary>
        public static List<QuerySetStmtObject> GetListQuerySetStmtObjs(string setStmt)
        {
            List<string> strSplits = SplitQuery(setStmt, false, ",");
            for (int i = 0; i < strSplits.Count; i++)
            {
                Regex r = new Regex(@"\s*set\s+", RegexOptions.IgnoreCase);
                strSplits[i] = r.Replace(strSplits[i], "");
            }
            TrimList(strSplits);
            strSplits.RemoveAll(x => string.IsNullOrWhiteSpace(x) || !x.StartsWith("@") || x.StartsWith("@_"));
            List<QuerySetStmtObject> bufferList = new List<QuerySetStmtObject>();
            for (int i = 0; i < strSplits.Count; i++)
            {
                QuerySetStmtObject qObj = new QuerySetStmtObject();
                qObj.Stmt = setStmt;
                qObj.Variable = strSplits[i].Split(new char[] { '=' }, 2).First();
                qObj.Value = strSplits[i].Split(new char[] { '=' }, 2).Last();
                bufferList.Add(qObj);
            }
            return bufferList;
        }

        ///<summary>Splits the given query string on the passed-in split string parameters. 
        ///DOES NOT split on the split strings when within single quotes, double quotes, parans, or case/if/concat statements.</summary>
        public static List<string> SplitQuery(string queryStr, bool includeDelimeters = false, params string[] listSplitStrs)
        {
            List<string> listStrSplit = new List<string>(); //returned list of strings.
            string totalStr = "";
            char quoteMode = '-'; //tracks whether we are currently within quotes.
            Stack<string> stackFuncs = new Stack<string>(); //tracks whether we are currently within a CASE, IF, or CONCAT statement.
            foreach (char c in queryStr)
            {
                if (quoteMode != '-')
                {
                    if (c == quoteMode)
                    {
                        quoteMode = '-';
                    }
                    totalStr += c;
                }
                else if (stackFuncs.Count > 0)
                {
                    if ((totalStr + c).ToLower().EndsWith("case"))
                    {
                        stackFuncs.Push("end");
                    }
                    else if ((totalStr + c).ToLower().EndsWith("("))
                    {
                        stackFuncs.Push(")");
                    }
                    else if ((totalStr + c).ToLower().EndsWith(stackFuncs.Peek()))
                    {
                        stackFuncs.Pop();
                    }
                    if (ListTools.In(c, '\'', '"'))
                    {
                        //Function has quotes. Set quote mode.
                        quoteMode = c;
                    }
                    //Only split string if we are not in quote mode and not in a function.
                    if (quoteMode == '-' && stackFuncs.Count == 0 && listSplitStrs.Contains(c.ToString()))
                    {
                        AddTotalStrToList(c, includeDelimeters, ref totalStr, ref listStrSplit);
                    }
                    else
                    {
                        totalStr += c;
                    }
                }
                else
                {
                    if ((totalStr + c).ToLower().EndsWith("case"))
                    {
                        stackFuncs.Push("end");
                        totalStr += c;
                    }
                    else if ((totalStr + c).ToLower().EndsWith("("))
                    {
                        stackFuncs.Push(")");
                        totalStr += c;
                    }
                    else if (listSplitStrs.Contains(c.ToString()))
                    {
                        AddTotalStrToList(c, includeDelimeters, ref totalStr, ref listStrSplit);
                    }
                    else
                    {
                        if (c == '\'' || c == '"')
                        {
                            quoteMode = c;
                        }
                        totalStr += c;
                    }
                }
            }
            listStrSplit.Add(totalStr);
            return listStrSplit;
        }

        ///<summary>Adds the totalStr to the listStrSplit passed in and then clears the totalStr.  Sets totalStr to the delimeter if includeDelimeters
        ///is true.</summary>
        private static void AddTotalStrToList(char c, bool includeDelimeters, ref string totalStr, ref List<string> listStrSplit)
        {
            if (includeDelimeters)
            {
                totalStr += c;
            }
            listStrSplit.Add(totalStr);
            totalStr = "";
        }

        ///<summary>Returns a string with SQL comments removed.
        ///E.g. removes /**/ and -- SQL comments from the query passed in.</summary>
        public static string RemoveSQLComments(string queryText)
        {
            Regex blockComments = new Regex(@"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/");
            Regex lineComments = new Regex(@"--.*");
            string retVal = blockComments.Replace(queryText, "");
            retVal = lineComments.Replace(retVal, "");
            return retVal;
        }

        ///<summary>Helper method to remove leading and trailing spaces from every element in a list of strings.</summary>
        public static void TrimList(List<string> trimList)
        {
            for (int i = 0; i < trimList.Count; i++)
            {
                trimList[i] = trimList[i].Trim();
            }
        }

        ///<summary>Takes the passed-in query text and returns a list of SET statements within the query. Pass in the entire query.</summary>
        public static List<string> ParseSetStatements(string queryText)
        {
            queryText = RemoveSQLComments(queryText);
            List<string> listParsedSetStmt = new List<string>();//Returned list of set statements.
            foreach (string smt in SplitQuery(queryText, true, ";"))
            {
                //The list of set statements returned from SplitQuery will include the delimiter(";"). Split each of the set statements using the c# splitter 
                //with the delimiter ";" again incase the query's set statements have invalid apostrophes. We can do this because we don't allow users to enter
                //";" inside a SET statement value.
                listParsedSetStmt.AddRange(smt.Split(";", StringSplitOptions.RemoveEmptyEntries));
            };
            TrimList(listParsedSetStmt);
            listParsedSetStmt.RemoveAll(x => string.IsNullOrEmpty(x));
            return listParsedSetStmt.Where(x => x.ToLower().StartsWith("set ")).ToList();
        }
    }

    ///<summary>A tiny class that contains a single SET statement's variable, value, and the entire statement.</summary>
    public class QuerySetStmtObject
    {
        public string Variable;
        public string Value;
        public string Stmt;
    }
}













