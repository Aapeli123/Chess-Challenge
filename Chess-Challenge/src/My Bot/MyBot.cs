using ChessChallenge.API;
using System;
using System.Collections.Generic;
public class MyBot : IChessBot
{
    Dictionary<ulong, float> lookup = new();
    Random random = new();
    Dictionary<PieceType,float> PieceEvals = new(){
        {PieceType.Pawn, 1},
        {PieceType.Bishop, 3},
        {PieceType.Knight, 3},
        {PieceType.Rook, 5},
        {PieceType.Queen, 9},
        {PieceType.King, 30},
    };
    public Move Think(Board board, Timer timer)
    {
        var eval = EvaluateBoard(board);
        System.Console.WriteLine(eval);
        Move[] moves = board.GetLegalMoves();
        foreach (Move m in moves) {
            board.MakeMove(m);
            board.GetFenString();

            board.UndoMove(m);

        }
        return moves[0];
    }

    private Move MonteCarloSearch(Board b, int depth) {
        var move = MakeRandomMove(b);
        MonteCarloSearch(b, depth - 1);
        b.UndoMove(move);
        if(depth == 0) return move;
        return move;
    }

    private Move MakeRandomMove(Board b) {
        Span<Move> moves = new();
        b.GetLegalMovesNonAlloc(ref moves, false);
        var index = random.Next(0, moves.Length);
        var move = moves[index];
        b.MakeMove(move);
        return move;
    }

    private float EvaluateBoard(Board b) {
        var pLists = b.GetAllPieceLists();
        float eval = 0;
        foreach(var plist in pLists) {
            foreach (var piece in plist)
            {
                var colorM = piece.IsWhite ?  1 : -1;
                var pValue = PieceEvals[piece.PieceType] * colorM;
                eval += pValue;
            }
        }
        
        return eval; 
    }
}