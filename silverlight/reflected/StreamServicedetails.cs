public static string GetStreamServiceUrlHash(string streamUrl, int timeStampSeconds)
{
	if ((sessionKey != null) && (sessionKey.Length > 2))
	{
		int result = 0;
		int.TryParse(sessionKey[0], out result);
		string str = (result + timeStampSeconds).ToString("X");
		string hashString = MD5Core.GetHashString(sessionKey[2] + streamUrl + str);
		return (DataControl.episodeCurrent.ServiceUrl + hashString + "/" + str + streamUrl);
	}
	return null;
}

/***
 * sessionKey example:
 * $ echo 'MTMwMzQ5MjI3N3xOUE9VR1NMIDEuMHxoNnJjbXNJZnxnb25laTFBaQ==' | base64 --decode; echo
 * 1303492277|NPOUGSL 1.0|h6rcmsIf|gonei1Ai
 */

/* What happens */
int result = 0; 
// (temp variable)
int.TryParse(sessionKey[0], out result); 
// try to get an integer value from sessionKey[0], put an integer value in result, if applicable
string str = (result + timeStampSeconds).ToString("X"); 
// parse (result + TimeStampSeconds) in integer form as a string
string hashString = MD5Core.GetHashString(sessionKey[2] + streamUrl + str); 
// get MD5 hash from (sessionKey[2] + streamUrl + str).


