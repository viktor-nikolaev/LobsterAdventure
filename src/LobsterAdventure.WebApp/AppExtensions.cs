using System;
using Microsoft.AspNetCore.Http;

namespace LobsterAdventure.WebApp.GQL
{
  public static class AppExtensions
  {
    public static string GetUserId(this HttpContextAccessor httpContextAccessor)
    {
      return httpContextAccessor.HttpContext.Request.Cookies["user_id"] ??
             throw new InvalidOperationException("Cookie user_id not set");
    }
  }
}