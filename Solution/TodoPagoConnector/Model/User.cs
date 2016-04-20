using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TodoPagoConnector.Model
{
    public class User
    {

    private String user;	
	private String password;
	private String merchant;	
	private String apiKey;
	
	public User(){}

	public User(String user, String password){
		this.user = user;
		this.password = password;
	}

	public String getMerchant() {
		return merchant;
	}

	public String getApiKey() {
		return apiKey;
	}

	
	public void setPassword(String password) {
		this.password = password;
	}

	public String getPassword() {
		return password;
	}

	public void setMerchant(String merchant) {
		this.merchant = merchant;
	}

	public void setApiKey(String apiKey) {
		this.apiKey = apiKey;
	}

	public String getUser() {
		return user;
	}

	public void setUser(String user) {
		this.user = user;
	}

	
	public String toString() {
		return "User [ merchant=" + merchant + ", apiKey=" + apiKey + " ]";
	}

    public Dictionary<string, string> toDictionary()
    {

        Dictionary<string, string> dic = new Dictionary<string, string>();

        dic.Add("USUARIO", this.user);
        dic.Add("CLAVE", this.password);

        return dic;
    }


    }
}
