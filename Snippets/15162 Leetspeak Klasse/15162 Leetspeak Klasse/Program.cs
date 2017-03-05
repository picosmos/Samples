using System;
using System.Collections.Generic;
using System.Text;

namespace _15162_Leetspeak_Klasse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class LeetGenerator
    {
        readonly Dictionary<char, string[]> _dict;
        readonly Random _rnd;

        public string GetLeetString(string input)
        {
            var sb = new StringBuilder();
            input = input.ToUpper().Replace(" ", "  ");

            foreach (var c in input)
            {
                if (_dict.ContainsKey(c))
                    sb.Append(_dict[c][_rnd.Next(_dict[c].Length)]);
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }

        public LeetGenerator()
        {
            _rnd = new Random();
            _dict = new Dictionary<char, string[]>
            {
                {'A', new [] {"4", "@", "/\\", "/-\\", "?", "^", "α", "λ"}},
                {'B', new [] {"8", "|3", "ß", "l³", "13", "I3", "J3"}},
                {'C', new [] {"(", "[", "<", "©", "¢"}},
                {'D', new [] {"|)", "|]", "Ð", "đ", "1)"}},
                {'E', new [] {"3", "€", "&", "£", "ε"}},
                {'F', new [] {"|=", "PH", "|*|-|", "|\"", "ƒ", "l²"}},
                {'G', new [] {"6", "&", "9"}},
                {'H', new [] {"4", "|-|", "#", "}{", "]-[", "/-/", ")-("}},
                {'I', new [] {"!", "1", "|", "][", "ỉ"}},
                {'J', new [] {"_|", "¿"}},
                {'K', new [] {"|<", "|{", "|(", "X"}},
                {'L', new [] {"1", "|_", "£", "|", "][_"}},
                {'M', new [] {"/\\/\\", "/v\\", "|V|", "]V[", "|\\/|", "AA", "[]V[]", "|11", "/|\\", "^^", "(V)", "|Y|","!\\/!"} },
                {'N', new [] {"|\\|", "/\\/", "/V", "|V", "/\\/", "|1", "2", "?", "(\\)", "11", "r", "!\\!"}},
                {'O', new [] {"0", "9", "()", "[]", "*", "°", "<>", "ø", "{[]}"}},
                {'P', new [] {"9", "|°", "p", "|>", "|*", "[]D", "][D", "|²", "|?", "|D"}},
                {'Q', new [] {"0_", "0,"}},
                {'R', new [] {"2", "|2", "1²", "®", "?", "я", "12", ".-"}},
                {'S', new [] {"5", "$", "§", "?", "ŝ", "ş"}},
                {'T', new [] {"7", "+", "†", "']['", "|"}},
                {'U', new [] {"|_|", "µ", "[_]", "v"}},
                {'V', new [] {"\\/", "|/", "\\|", "\'"}},
                {'W', new [] {"\\/\\/", "VV", "\\A/", "\\'", "uu", "\\^/", "\\|/", "uJ"}},
                {'X', new [] {"><", ")(", "}{", "%", "?", "×", "]["}},
                {'Y', new [] {"`/", "°/", "¥"}},
                {'Z', new [] {"z", "2", "\"/_"}},
                {'Ä', new [] {"43", "°A°", "°4°"}},
                {'Ö', new [] {"03", "°O°"}},
                {'Ü', new [] {"|_|3", "°U°"}}
            };
        }
    }