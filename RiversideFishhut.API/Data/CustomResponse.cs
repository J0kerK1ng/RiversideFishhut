namespace RiversideFishhut.API.Data
{
	public class CustomResponse
	{
		public int Status { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }

		public CustomResponse(int status, string message, object data)
		{
			Status = status;
			Message = message;
			Data = data;
		}

	}
}
