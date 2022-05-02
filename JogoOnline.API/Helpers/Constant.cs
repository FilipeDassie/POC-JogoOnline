namespace JogoOnline.API.Helpers
{
    public class Constant
    {
        #region Database connections

        public const string Key_ConnectionStrings_JogoOnline = "JogoOnline";

        #endregion

        #region App Settings

        public const string Key_AppSettings_Cron_Expression_Process_Game_Result = "AppSettings:CronExpressionProcessGameResult";
        public const string Key_AppSettings_RedisCache_Connection = "AppSettings:RedisCacheConnection";

        #endregion
    }
}