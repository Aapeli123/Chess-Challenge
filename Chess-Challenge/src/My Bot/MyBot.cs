using ChessChallenge.API;
using System.Collections.Generic;
public class MyBot : IChessBot
{
    Dictionary<string, Move> lookup;
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        foreach (Move m in moves) {
            board.MakeMove(m);
            board.GetFenString();
            board.UndoMove(m);
        }
        return moves[0];
    }
}