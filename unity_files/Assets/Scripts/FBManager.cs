using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FBManager{

	public  bool isLoggedIn;
	public  Texture2D texture;
	public  bool hasImage;
	public  bool hasUserName;
	public  Dictionary<string, object> profile = null;

	public FBManager()
	{
		FB.Init (SetInit, OnHideUnity);
		isLoggedIn = false;
		hasImage = false;
	}
	
	private void SetInit()
	{
		Debug.Log ("FB Init done.");

		if(FB.IsLoggedIn)
		{
			//DealWithFBMenus(true);
			Debug.Log ("FB Logged In");
		}else{
			//DealWithFBMenus(false);
		}
		
	}
	
	private void OnHideUnity(bool isGameShown)
	{
		
		if(!isGameShown)
		{
			Time.timeScale = 0;
		}else{
			Time.timeScale = 1;
		}
		
	}
	
	public void FBlogin()
	{
		var perms = new List<string>(){"email"};
		FB.LogInWithReadPermissions(perms, AuthCallback);
		

		Debug.Log ("Attempt FB Login");
	}
	
	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			/*
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
			*/
			isLoggedIn = true;

			FB.API (Util.GetPictureURL("me", 128, 128), HttpMethod.GET, DealWithProfilePicture);
			FB.API ("/me?fields=first_name,last_name,email", HttpMethod.GET, DealWithUserName);

			Debug.Log("User Logged in");
		} else {
			Debug.Log("User cancelled login");
		}
	}

	void DealWithProfilePicture(IGraphResult result)
	{
		
		if(result.Error != null)
		{
			Debug.Log ("problem with getting profile picture");
			
			FB.API (Util.GetPictureURL("me", 128, 128), HttpMethod.GET, DealWithProfilePicture);
			return;
		}

		texture = result.Texture;
		hasImage = true;
		Debug.Log ("Got image");
		
	}

	void DealWithUserName(IGraphResult result)
	{
		if(result.Error != null)
		{
			Debug.Log ("problem with getting profile picture");
			
			FB.API ("/me?fields=first_name,last_name,email", HttpMethod.GET, DealWithUserName);
			return;
		}

		profile = (Dictionary<string,object>)result.ResultDictionary;
		
		Debug.Log ("Profile: first name: " + profile["first_name"]);
		Debug.Log ("Profile: last name: " + profile["last_name"]);
		Debug.Log ("Profile: id: " + profile["id"]);
		Debug.Log ("Profile: email: " + profile["email"]);

		hasUserName = true;

		
		
	}

	public void ShareWithFriends()
	{

		FB.FeedShare(
			toId: "",
			link: null,
			linkName: "MineSweeper+",
			linkCaption: "MineSweeper+",
			linkDescription: "Woo MineSweeper!?",
			picture: null,
			mediaSource: "",
			callback: ShareCallBack
			);
	}
	private void ShareCallBack (IShareResult result) {
		if (result != null) {

			Debug.Log("Share Failed");
		} else {

			Debug.Log("Shared Content!");
		}
	}
}
