namespace RiversideFishhut.API.Data
{
	public class CustomTokenResponse
	{
		public int Status { get; set; }
		public string Message { get; set; }
		public object Data { get; set; }

		public CustomTokenResponse(int status, string message, object data)
		{
			Status = status;
			Message = message;
			Data = data;
		}
	}

	public class TokenException : Exception
	{
		public CustomTokenResponse CustomTokenResponse { get; set; }

		public TokenException(CustomTokenResponse customTokenResponse)
		{
			CustomTokenResponse = customTokenResponse;
		}
	}
}

