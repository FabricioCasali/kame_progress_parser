using System;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;

namespace KameProgressParser
{
    public class ErrorHandling : DefaultErrorStrategy
    {


        public override bool InErrorRecoveryMode(Parser recognizer)
        {
            return base.InErrorRecoveryMode(recognizer);
        }

        public override void Recover(Parser recognizer, RecognitionException e)
        {
            base.Recover(recognizer, e);
        }

        public override IToken RecoverInline(Parser recognizer)
        {
            return base.RecoverInline(recognizer);
        }

        public override void ReportError(Parser recognizer, RecognitionException e)
        {
            base.ReportError(recognizer, e);
        }

        public override void ReportMatch(Parser recognizer)
        {
            base.ReportMatch(recognizer);
        }

        public override void Reset(Parser recognizer)
        {
            base.Reset(recognizer);
        }

        public override void Sync(Parser recognizer)
        {
            base.Sync(recognizer);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void BeginErrorCondition(Parser recognizer)
        {
            base.BeginErrorCondition(recognizer);
        }

        protected override IToken ConstructToken(ITokenSource tokenSource, int expectedTokenType, string tokenText, IToken current)
        {
            return base.ConstructToken(tokenSource, expectedTokenType, tokenText, current);
        }

        protected override void ConsumeUntil(Parser recognizer, IntervalSet set)
        {
            base.ConsumeUntil(recognizer, set);
        }

        protected override void EndErrorCondition(Parser recognizer)
        {
            base.EndErrorCondition(recognizer);
        }

        [return: NotNull]
        protected override string EscapeWSAndQuote(string s)
        {
            return base.EscapeWSAndQuote(s);
        }

        [return: NotNull]
        protected override IntervalSet GetErrorRecoverySet(Parser recognizer)
        {
            return base.GetErrorRecoverySet(recognizer);
        }

        [return: NotNull]
        protected override IntervalSet GetExpectedTokens(Parser recognizer)
        {
            return base.GetExpectedTokens(recognizer);
        }

        [return: NotNull]
        protected override IToken GetMissingSymbol(Parser recognizer)
        {
            return base.GetMissingSymbol(recognizer);
        }

        protected override string GetSymbolText(IToken symbol)
        {
            return base.GetSymbolText(symbol);
        }

        protected override int GetSymbolType(IToken symbol)
        {
            return base.GetSymbolType(symbol);
        }

        protected override string GetTokenErrorDisplay(IToken t)
        {
            return base.GetTokenErrorDisplay(t);
        }

        protected override void NotifyErrorListeners(Parser recognizer, string message, RecognitionException e)
        {
            base.NotifyErrorListeners(recognizer, message, e);
        }

        protected override void ReportFailedPredicate(Parser recognizer, FailedPredicateException e)
        {
            base.ReportFailedPredicate(recognizer, e);
        }

        protected override void ReportInputMismatch(Parser recognizer, InputMismatchException e)
        {
            base.ReportInputMismatch(recognizer, e);
        }

        protected override void ReportMissingToken(Parser recognizer)
        {
            base.ReportMissingToken(recognizer);
        }

        protected override void ReportNoViableAlternative(Parser recognizer, NoViableAltException e)
        {
            base.ReportNoViableAlternative(recognizer, e);
        }

        protected override void ReportUnwantedToken(Parser recognizer)
        {
            base.ReportUnwantedToken(recognizer);
        }

        [return: Nullable]
        protected override IToken SingleTokenDeletion(Parser recognizer)
        {
            return base.SingleTokenDeletion(recognizer);
        }

        protected override bool SingleTokenInsertion(Parser recognizer)
        {
            return base.SingleTokenInsertion(recognizer);
        }
    }


}
