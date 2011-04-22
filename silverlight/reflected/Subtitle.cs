public static void DownloadSubtitleData()
{
    string episodeId = DataControl.episodeCurrent.EpisodeId;
    string episodeSubtitleUrl = DataControl.EpisodeSubtitleUrl;
    Config config = App.Configuration.GetConfig(DataControl.embeddingAlias.ToString());
    string subtitleApplicationName = "player";
    if (!string.IsNullOrEmpty(config.SubtitleApplicationName))
    {
        subtitleApplicationName = config.SubtitleApplicationName;
    }
    string subtitleUrlHashMethod = DataControl.SubtitleUrlHashMethod;
    if (subtitleUrlHashMethod != null)
    {
        if (!(subtitleUrlHashMethod == "none"))
        {
            if ((subtitleUrlHashMethod == "subtitleSilverlightSecurity1") && ((Session.sessionKey != null) && (Session.sessionKey.Length > 3)))
            {
                int result = 0;
                int.TryParse(Session.sessionKey[0], out result);
                int num2 = result + DataControl.videoController.InitDuration;
                string str4 = num2.ToString("X").ToLower();
                string str5 = "aflevering/" + episodeId + "/format/sami";
                string str7 = MD5Core.GetHashString(Session.sessionKey[3] + str5 + str4 + subtitleApplicationName).ToLower();
                string str10 = episodeSubtitleUrl;
                episodeSubtitleUrl = str10 + subtitleApplicationName + "/" + str7 + "/" + str4 + "/" + str5;
            }
        }
        else
        {
            episodeSubtitleUrl = episodeSubtitleUrl + episodeId;
        }
    }
    string subtitleStatus = DataControl.subtitleStatus;
    DataControl.subtitleStatus = "loading";
    API.DispatchEvent(API.EventType.PropertyChanged, API.PropertyName.subtitleStatus, subtitleStatus, DataControl.subtitleStatus);
    if ((config != null) && !string.IsNullOrEmpty(config.InfoSubtitlesLoading))
    {
        DataControl.ShowLoadingProgress(config.InfoSubtitlesLoading);
    }
    else
    {
        DataControl.ShowLoadingProgress(Strings.LoadingMessageSubtitle);
    }
    WebClient client = new WebClient();
    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Subtitle.SubtitleData_DownloadProgressChanged);
    client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Subtitle.SubtitleData_DownloadStringCompleted);
    client.DownloadStringAsync(new Uri(UriUtils.FormatWithRoot(episodeSubtitleUrl), UriKind.RelativeOrAbsolute));
}
