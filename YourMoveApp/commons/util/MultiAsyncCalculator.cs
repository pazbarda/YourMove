namespace YourMoveApp.commons.util
{
    public class MultiAsyncCalculator<A, R>
    {
        private readonly List<Func<R>> _inputFuncs;
        private readonly Func<List<R>, R> _resultCalculationFunc;

        public MultiAsyncCalculator(List<Func<R>> inputFuncs, Func<List<R>, R> resultCalculationAction)
        {
            this._inputFuncs = inputFuncs;
            this._resultCalculationFunc = resultCalculationAction;
        }

        public async Task<R> CalculateAsync()
        {
            List<R> results = new();
            List<Task> tasks = new();
            foreach (Func<R> inputFunc in _inputFuncs)
            {
                Action action = new(() => {
                    results.Add(inputFunc());
                });                
                tasks.Add(Task.Run(() => action.Invoke()));
            }
            await Task.WhenAll(tasks);
            return _resultCalculationFunc(results);
        }
    }
}