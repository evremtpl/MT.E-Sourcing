namespace MT.E_Sourcing.WebApp.Core.ResultModels
{
    public  interface IResult
    {

        public bool IsSuccess { get; set; }

        string Message { get; set; }
    }
}
