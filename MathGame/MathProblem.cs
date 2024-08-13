using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame;

internal abstract class MathProblem
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Operator { get; set; }
    public int Result { get; set; }
    public int PlayerAnswer { get; set; }
    public bool WasCorrect {  get; set; }

    public void MakeAttempt(int playerAnswer)
    {
        PlayerAnswer = playerAnswer;
        WasCorrect = (playerAnswer == this.Result);
    }

    public string ToStringPrompt()
    {
        return $"{X} {Operator} {Y} = ";
    }

    public string ToStringFull()
    {
        return ToStringPrompt() + PlayerAnswer;
    }

}
