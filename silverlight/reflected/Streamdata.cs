private static void DownloadStreamData(bool refresh)
{
    isRefresh = refresh;
    string fragmentId = null;
    string uri = null;
    string followNumber = null;
    if (!string.IsNullOrEmpty(DataControl.episodeCurrent.FragmentId))
    {
        fragmentId = DataControl.episodeCurrent.FragmentId;
        uri = DataControl.FragmentStreamUrl;
    }
    else if (!string.IsNullOrEmpty(DataControl.episodeCurrent.EpisodeId))
    {
        fragmentId = DataControl.episodeCurrent.EpisodeId;
        uri = DataControl.EpisodeStreamUrl;
    }
    if (!string.IsNullOrEmpty(DataControl.episodeCurrent.FollowNumber))
    {
        followNumber = DataControl.episodeCurrent.FollowNumber;
    }
    string streamUrlHashMethod = DataControl.StreamUrlHashMethod;
    if (streamUrlHashMethod != null)
    {
        if (!(streamUrlHashMethod == "none"))
        {
            if ((streamUrlHashMethod == "streamSilverlightSecurity1") && ((Session.sessionKey != null) && (Session.sessionKey.Length > 1)))
            {
                string hashString = MD5Core.GetHashString(fragmentId + "|" + Session.sessionKey[1]);
                if (followNumber == null)
                {
                    uri = uri + fragmentId + "/" + hashString;
                }
                else
                {
                    string str7 = uri;
                    uri = str7 + fragmentId + "/fragment/" + followNumber + "/" + hashString;
                }
            }
        }
        else if (followNumber == null)
        {
            uri = uri + fragmentId;
        }
        else
        {
            uri = uri + fragmentId + "/" + followNumber;
        }
    }
    Config config = App.Configuration.GetConfig(DataControl.embeddingAlias.ToString());
    if ((config != null) && !string.IsNullOrEmpty(config.InfoStreamLoading))
    {
        DataControl.ShowLoadingProgress(config.InfoStreamLoading);
    }
    else
    {
        DataControl.ShowLoadingProgress(Strings.LoadingMessageStream);
    }
    WebClient client = new WebClient();
    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Streamdata.StreamData_DownloadProgressChanged);
    client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(Streamdata.StreamData_DownloadStringCompleted);
    client.DownloadStringAsync(new Uri(UriUtils.FormatWithRoot(uri), UriKind.RelativeOrAbsolute));
}


