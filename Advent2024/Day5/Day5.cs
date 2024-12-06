using System.Diagnostics;

namespace Advent2024.Day5;

public class Day5
{
    private static readonly string _exInput =
        "47|53\r\n97|13\r\n97|61\r\n97|47\r\n75|29\r\n61|13\r\n75|53\r\n29|13\r\n97|29\r\n53|29\r\n61|53\r\n97|53\r\n61|29\r\n47|13\r\n75|47\r\n97|75\r\n47|61\r\n75|61\r\n47|29\r\n75|13\r\n53|13\r\n\r\n75,47,61,53,29\r\n97,61,53,29,13\r\n75,29,13\r\n75,97,47,61,53\r\n61,13,29\r\n97,13,75,29,47";

    public static void Solution(out int part1, out int part2)
    {
        var input = Input.Read("Day5");

        var sections = input.Split(Environment.NewLine + Environment.NewLine);
        var pageOrderingRules = sections[0].Split(Environment.NewLine).ToList();
        var updates = sections[1].Split(Environment.NewLine).ToList();

        int sumRightPages = 0;
        int sumWrongPages = 0;
        string currentPage = "";
        string nextPage = "";
        for (var i = 0; i < updates.Count; i++)
        {
            var pages = updates[i].Split(',');
            bool isRightOrdered = true;
            List<(int, int)> rotationsToFixPage = new List<(int, int)>();
            
            for (var j = 0; j < pages.Length - 1; j++)
            {
                currentPage = pages[j];
                nextPage = pages[j + 1];
                if (!CheckRule(pageOrderingRules, currentPage, nextPage, null,null))
                {
                    isRightOrdered = false;
                    rotationsToFixPage.Add((j, j+1));
                }
            }

            if (isRightOrdered)
            {
                int middlePage = (int)Math.Floor(pages.Length * 0.5f);
                sumRightPages += int.Parse(pages[middlePage]);
            }
            else
            {
                var isWrongFixed = false;
                do
                {
                    Console.WriteLine("Before: " + string.Join(",", pages));
                    for (var t = 0; t < rotationsToFixPage.Count; t++)
                    {
                        (pages[rotationsToFixPage[t].Item2], pages[rotationsToFixPage[t].Item1]) = (pages[rotationsToFixPage[t].Item1], pages[rotationsToFixPage[t].Item2]);
                    }
                    Console.WriteLine("After: " + string.Join(",",pages));
   
                    rotationsToFixPage.Clear();
                    bool isFixed = true;
                    for (var j = 0; j < pages.Length - 1; j++)
                    {
                        currentPage = pages[j];
                        nextPage = pages[j + 1];
                        if (!CheckRule(pageOrderingRules, currentPage, nextPage, null, null))
                        {
                            isFixed = isFixed & true;
                        }
                        else
                        {
                            isFixed = isFixed & false;
                            rotationsToFixPage.Add((j, j + 1));
                        }
                            
                    }

                    isWrongFixed = isFixed;

                } while (!isWrongFixed);
                
                
                int middlePage = (int)Math.Floor(pages.Length * 0.5f);
                sumWrongPages += int.Parse(pages[middlePage]);
            }
            
        }
          
        
        part1 = sumRightPages;
        part2 = sumWrongPages;
    }
    
    public static bool CheckRule(List<string> rules,string currentPage,string afterPage, Stack<(string currentPage, string afterPage, int pageIndex)> searching, HashSet<string> visited, int continueSearchIndex = -1)
    {
        if (searching == null) searching = new Stack<(string currentPage,string afterPage, int pageIndex)>();
        if (visited == null) visited = new HashSet<string>();
        
        if (visited.Contains(currentPage))
        {
            var previousState = searching.Pop();
            return CheckRule(rules, previousState.currentPage, previousState.afterPage, searching,visited, previousState.pageIndex);
        }
        
        for (var index = continueSearchIndex + 1; index < rules.Count; index++)
        {
            var rule = rules[index];
            var firstPage = rule.Substring(0, 2);
            var nextPage = rule.Substring(3, 2);
            if (firstPage == currentPage)
            {
                if (nextPage == afterPage)
                {
                    return true;
                }
                
                
                searching.Push((currentPage, afterPage, index));
                // DFS => BFS change
                // return CheckRule(rules, nextPage, afterPage, searching);
            }
            else if (firstPage == afterPage && nextPage == currentPage)
            {
                return false;
            } 
        }

        visited.Add(currentPage);

        if (searching.Count > 0)
        {
            var previousState = searching.Pop();
            return CheckRule(rules, previousState.currentPage, previousState.afterPage, searching, visited, previousState.pageIndex);
        }

        return false;

    }
}