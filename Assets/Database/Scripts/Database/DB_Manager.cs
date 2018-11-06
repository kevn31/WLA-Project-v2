using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DB_Manager : MonoBehaviour
{
    public static DB_Manager instance;
    [Header("DATABASE")]
    public string host;
    public string database, username, password;
    [Header("REGISTER")]
    public Canvas CanvasRegister;
    public InputField RPseudo;
    public InputField REmail, RPassword, RConfirmation, RFirstName, RLastName, RPhoneNumber, RAddress, RCity, RZipCode;
    List<string> genders = new List<string>() { "Gender", "Man", "Woman" };
    public Dropdown gender;
    public Text selectedGender;
    public bool IsValidGender;
    List<string> countrys = new List<string>() { "Country", "Austria", "Belgium", "Brazil", "Bulgaria", "Canada", "China", "Croatia", "Cyprus", "Czech Republic", "Denmark", "Estonia", "Finland", "France", "Germany", "Greece", "Hungary", "India", "Ireland", "Israel", "Italy", "Japan", "Latvia", "Lithuania", "Luxembourg", "Malta", "Netherlands", "Poland", "Portugal", "Romania", "Russia", "Saudi Arabia", "Slovakia", "Slovenia", "South Korea", "Spain", "Sweden", "Swiss", "United Arab Emirats", "United Kingdom", "United States" };
    public Dropdown country;
    public Text selectedCountry;
    public bool IsValidCountry;
    List<string> birthmonths = new List<string>() { "Month", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
    public Dropdown birthmonth;
    public Text selectedBirthMonth;
    public bool IsValidBirthMonth;
    List<string> birthdays = new List<string>() { "Day", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
    public Dropdown birthday;
    public Text selectedBirthDay;
    public bool IsValidBirthDay;
    List<string> birthyears = new List<string>() { "Year", "1950", "1951", "1952", "1953", "1954", "1955", "1956", "1957", "1958", "1959", "1960", "1961", "1962", "1963", "1964", "1965", "1966", "1967", "1968", "1969", "1970", "1971", "1972", "1973", "1974", "1975", "1976", "1977", "1978", "1979", "1980", "1981", "1982", "1983", "1984", "1985", "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993", "1994", "1995", "1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011" };
    public Dropdown birthyear;
    public Text selectedBirthYear;
    public bool IsValidBirthYear;
    List<string> profiles = new List<string>() { "Player Profile", "I am a Pilot", "I am an Aero Fan", "I am a Gamer" };
    public Dropdown profile;
    public Text selectedProfile;
    public bool IsValidProfile;
    public Button readTerms;
    public bool IsValidTerms;
    public GameObject acceptTerms;
    public Canvas CanvasTerms;
    public Text RtxtInfos;
    MySqlConnection con;
    [Header("LOGIN")]
    public Canvas CanvasLogin;
    public InputField LPseudo;
    public InputField LPassword;
    public Text LtxtInfos;
    [Header("INFO USER")]
    public bool IBelogin;
    static bool IBeloginSave;
    public string IPseudo;
    static string IPseudoSave;

    private void Start()
    {
        IBelogin = false;
        connect_BDD();
        PopulateList();
    }

    void Awake()
    {
        CanvasLogin = GameObject.Find("CanvasLogin").GetComponent<Canvas>();
        PlayTampon();
       
       /* if (instance != null)
        {
          //  Destroy(gameObject);
        }

        else
        {
            instance = this;
         //   DontDestroyOnLoad(gameObject);
        }*/
    }

    void Update()
    {
    }

    void connect_BDD()
    {
        string cmd = "SERVER=" + host + ";" + "database=" + database + ";User ID=" + username + ";Password=" + password + ";Pooling=true;Charset=utf8";
        con = new MySqlConnection(cmd);
        
            try
            {
                con.Open();
            }

            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }

        if (IBelogin == true)
        {
            if (con.State.ToString() == "Open")
            { 
                try
                {
               //     con.Close();
                }

                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }
        }
    }

    #region Caracteres Requis
    bool IsValidLenght(string InputString, int LenghtString)
    {
        if (InputString.Length > LenghtString)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    bool IsValidLenghtMax(string InputString, int LenghtString)
    {
        if (InputString.Length < LenghtString)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    #endregion

    #region Validation Email Format
    bool IsValidEmail(string InputEmail)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(InputEmail);
        if (match.Success)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
    #endregion

    #region Validation Dropdown
    void PopulateList()
    {
        gender.AddOptions(genders);
        country.AddOptions(countrys);
        birthmonth.AddOptions(birthmonths);
        birthday.AddOptions(birthdays);
        birthyear.AddOptions(birthyears);
        profile.AddOptions(profiles);
    }

    public void Dropdown_IndexChangedGender(int index)
    {
        selectedGender.text = genders[index];

        if (index == 0)
        {
            selectedGender.color = Color.red;
            IsValidGender = false;
        }
        else
        {
            selectedGender.color = Color.white;
            IsValidGender = true;
        }
    }

    public void Dropdown_IndexChangedCountry(int index)
    {
        selectedCountry.text = countrys[index];
        if (index == 0)
        {
            selectedCountry.color = Color.red;
            IsValidCountry = false;
        }
        else
        {
            selectedCountry.color = Color.white;
            IsValidCountry = true;
        }
    }

    public void Dropdown_IndexChangedBirthMonth(int index)
    {
        selectedBirthMonth.text = birthmonths[index];
        if (index == 0)
        {
            selectedBirthMonth.color = Color.red;
            IsValidBirthMonth = false;
        }
        else
        {
            selectedBirthMonth.color = Color.white;
            IsValidBirthMonth = true;
        }
    }

    public void Dropdown_IndexChangedBirthDay(int index)
    {
        selectedBirthDay.text = birthdays[index];
        if (index == 0)
        {
            selectedBirthDay.color = Color.red;
            IsValidBirthDay = false;
        }
        else
        {
            selectedBirthDay.color = Color.white;
            IsValidBirthDay = true;
        }
    }

    public void Dropdown_IndexChangedBirthYear(int index)
    {
        selectedBirthYear.text = birthyears[index];
        if (index == 0)
        {
            selectedBirthYear.color = Color.red;
            IsValidBirthYear = false;
        }
        else
        {
            selectedBirthYear.color = Color.white;
            IsValidBirthYear = true;
        }
    }

    public void Dropdown_IndexChangedProfile(int index)
    {
        selectedProfile.text = profiles[index];
        if (index == 0)
        {
            selectedProfile.color = Color.red;
            IsValidProfile = false;
        }
        else
        {
            selectedProfile.color = Color.white;
            IsValidProfile = true;
        }
    }
    #endregion

    #region Terms
    public void Toggle_Terms()
    {
        bool validTerms = acceptTerms.GetComponent<Toggle>().isOn;
        if (validTerms)
        {
            IsValidTerms = true;
        }
        else IsValidTerms = false;

    }

    public void ShowTerms()
    {
        CanvasRegister.gameObject.SetActive(false);
        CanvasTerms.gameObject.SetActive(true);
    }
    #endregion

    #region White Clic
    public void onClicInput ()
    {
        ColorBlock cbPseudo = RPseudo.colors;
        cbPseudo.normalColor = Color.white;
        RPseudo.colors = cbPseudo;

        ColorBlock cbEmail = REmail.colors;
        cbEmail.normalColor = Color.white;
        REmail.colors = cbEmail;

        ColorBlock cbPassword = RPassword.colors;
        cbPassword.normalColor = Color.white;
        RPassword.colors = cbPassword;

        ColorBlock cbFirstName = RFirstName.colors;
        cbFirstName.normalColor = Color.white;
        RFirstName.colors = cbFirstName;

        ColorBlock cbLastName = RLastName.colors;
        cbLastName.normalColor = Color.white;
        RLastName.colors = cbLastName;

        ColorBlock cbGender = gender.colors;
        cbGender.normalColor = Color.white;
        gender.colors = cbGender;

        ColorBlock cbPhoneNumber = RPhoneNumber.colors;
        cbPhoneNumber.normalColor = Color.white;
        RPhoneNumber.colors = cbPhoneNumber;

        ColorBlock cbCountry = country.colors;
        cbCountry.normalColor = Color.white;
        country.colors = cbCountry;

        ColorBlock cbAddress = RAddress.colors;
        cbAddress.normalColor = Color.white;
        RAddress.colors = cbAddress;

        ColorBlock cbCity = RCity.colors;
        cbCity.normalColor = Color.white;
        RCity.colors = cbCity;

        ColorBlock cbZipCode = RZipCode.colors;
        cbZipCode.normalColor = Color.white;
        RZipCode.colors = cbZipCode;

        ColorBlock cbBirthMonth = birthmonth.colors;
        cbBirthMonth.normalColor = Color.white;
        birthmonth.colors = cbBirthMonth;

        ColorBlock cbBirthDay = birthday.colors;
        cbBirthDay.normalColor = Color.white;
        birthday.colors = cbBirthDay;

        ColorBlock cbBirthYear = birthyear.colors;
        cbBirthYear.normalColor = Color.white;
        birthyear.colors = cbBirthYear;

        ColorBlock cbProfile = profile.colors;
        cbProfile.normalColor = Color.white;
        profile.colors = cbProfile;

        ColorBlock cbReadTerms = readTerms.colors;
        cbReadTerms.normalColor = Color.white;
        readTerms.colors = cbReadTerms;
    }
    #endregion

    bool IsValid()
    {
        #region Pseudo
        ColorBlock cbPseudo = RPseudo.colors;

        if (!IsValidLenght(RPseudo.text, 2))
        {
            cbPseudo.normalColor = Color.red;
            RtxtInfos.text = "Invalid Pseudo";
            Handheld.Vibrate();
            RPseudo.colors = cbPseudo;
            return false;
        }

        if (!IsValidLenghtMax(RPseudo.text, 16))
        {
            cbPseudo.normalColor = Color.red;
            RtxtInfos.text = "Invalid Pseudo";
            Handheld.Vibrate();
            RPseudo.colors = cbPseudo;
            return false;
        }

        else
        {
            cbPseudo.normalColor = Color.white;
            RtxtInfos.text = "";
            RPseudo.colors = cbPseudo;
        }

        //Verification existance pseudo
        try
        {
            connect_BDD();
            MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `users` WHERE `pseudo`='" + RPseudo.text + "'", con);
            MySqlDataReader MyReader = CmdSql.ExecuteReader();
            string data = null;

            while (MyReader.Read())
            {
                data = MyReader["password"].ToString();

                if (data != null)
                {
                    cbPseudo.normalColor = Color.red;
                    RtxtInfos.text = "Pseudo Already Used";
                    Handheld.Vibrate();
                    RPseudo.colors = cbPseudo;
                    MyReader.Close();
                    return false;
                }

               else
                {
                    cbPseudo.normalColor = Color.white;
                    RtxtInfos.text = "";
                    RPseudo.colors = cbPseudo;
                }
            }
            MyReader.Close();
        }
        catch (Exception Ex) { Debug.Log(Ex.ToString()); }
        #endregion

        #region Email
        //Verification de l'Email
        ColorBlock cbEmail = REmail.colors;

        if (!IsValidEmail(REmail.text))
        {
            cbEmail.normalColor = Color.red;
            RtxtInfos.text = "Invalid Email";
            Handheld.Vibrate();
            REmail.colors = cbEmail;
            return false;
        }
 
        else
        {
            RtxtInfos.text = "";
            cbEmail.normalColor = Color.white;
            REmail.colors = cbEmail;
        }

        //Verification existance email
        try
        {
            connect_BDD();
            MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `users` WHERE `email`='" + REmail.text + "'", con);
            MySqlDataReader MyReader = CmdSql.ExecuteReader();
            string data = null;

            while (MyReader.Read())
            {
                data = MyReader["password"].ToString();

                if (data != null)
                {
                    cbEmail.normalColor = Color.red;
                    RtxtInfos.text = "Email Already Used";
                    Handheld.Vibrate();
                    REmail.colors = cbEmail;
                    MyReader.Close();
                    return false;
                }

                else
                {
                    RtxtInfos.text = "";
                    cbEmail.normalColor = Color.white;
                    REmail.colors = cbEmail;
                }
            }
            MyReader.Close();
        }
        catch (Exception Ex) { Debug.Log(Ex.ToString()); }
        #endregion

        #region Password      
        ColorBlock cbPassword = RPassword.colors;

        if (!IsValidLenght(RPassword.text, 5))
        {
            cbPassword.normalColor = Color.red;
            RtxtInfos.text = "Invalid Password";
            Handheld.Vibrate();
            RPassword.colors = cbPassword;
            return false;
        }

        if (RPassword.text != RConfirmation.text)
        {
            cbPassword.normalColor = Color.red;
            RtxtInfos.text = "Different Password";
            Handheld.Vibrate();
            RPassword.colors = cbPassword;
            return false;
        }

        else
        {
            cbPassword.normalColor = Color.white;
            RtxtInfos.text = "";
            RPassword.colors = cbPassword;
        }
        #endregion

        #region First Name
        ColorBlock cbFirstName = RFirstName.colors;

        if (!IsValidLenght(RFirstName.text, 1))
        {
            cbFirstName.normalColor = Color.red;
            RtxtInfos.text = "Enter Your First Name";
            Handheld.Vibrate();
            RFirstName.colors = cbFirstName;
            return false;
        }

        else
        {
            cbFirstName.normalColor = Color.white;
            RtxtInfos.text = "";
            RFirstName.colors = cbFirstName;
        }
        #endregion

        #region Last Name
        //Last Name
        ColorBlock cbLastName = RLastName.colors;

        if (!IsValidLenght(RLastName.text, 1))
        {
            cbLastName.normalColor = Color.red;
            RtxtInfos.text = "Enter Your Last Name";
            Handheld.Vibrate();
            RLastName.colors = cbLastName;
            return false;
        }

        else
        {
            cbLastName.normalColor = Color.white;
            RtxtInfos.text = "";
            RLastName.colors = cbLastName;
        }
        #endregion

        #region Gender
        ColorBlock cbGender = gender.colors;

        if (IsValidGender == false)
        {
            cbGender.normalColor = Color.red;
            RtxtInfos.text = "Select Your Gender";
            Handheld.Vibrate();
            gender.colors = cbGender;
            return false;
        }

        else
        {
            cbGender.normalColor = Color.white;
            RtxtInfos.text = "";
            gender.colors = cbGender;
        }
        #endregion

        #region Phone Number
        ColorBlock cbPhoneNumber = RPhoneNumber.colors;

        if (!IsValidLenght(RPhoneNumber.text, 7))
        {
            cbPhoneNumber.normalColor = Color.red;
            RtxtInfos.text = "Enter Your Phone Number";
            Handheld.Vibrate();
            RPhoneNumber.colors = cbPhoneNumber;
            return false;
        }

        else
        {
            cbPhoneNumber.normalColor = Color.white;
            RtxtInfos.text = "";
            RPhoneNumber.colors = cbPhoneNumber;
        }
        #endregion

        #region Country
        ColorBlock cbCountry = country.colors;

        if (IsValidCountry == false)
        {
            cbCountry.normalColor = Color.red;
            RtxtInfos.text = "Enter Your Country";
            Handheld.Vibrate();
            country.colors = cbCountry;
            return false;
        }

        else
        {
            cbCountry.normalColor = Color.white;
            RtxtInfos.text = "";
            country.colors = cbCountry;
        }
        #endregion

        #region Address
        ColorBlock cbAddress = RAddress.colors;

        if (!IsValidLenght(RAddress.text, 7))
        {
            cbAddress.normalColor = Color.red;
            RtxtInfos.text = "Enter Your Full Address";
            Handheld.Vibrate();
            RAddress.colors = cbAddress;
            return false;
        }

        else
        {
            cbAddress.normalColor = Color.white;
            RtxtInfos.text = "";
            RAddress.colors = cbAddress;
        }
        #endregion

        #region City
        ColorBlock cbCity = RCity.colors;

        if (!IsValidLenght(RCity.text, 1))
        {
            cbCity.normalColor = Color.red;
            RtxtInfos.text = "Enter Your City";
            Handheld.Vibrate();
            RCity.colors = cbCity;
            return false;
        }

        else
        {
            cbCity.normalColor = Color.white;
            RtxtInfos.text = "";
            RCity.colors = cbCity;
        }
        #endregion

        #region Zip Code
        ColorBlock cbZipCode = RZipCode.colors;

        if (!IsValidLenght(RZipCode.text, 2))
        {
            cbZipCode.normalColor = Color.red;
            RtxtInfos.text = "Enter Your Zip Code";
            Handheld.Vibrate();
            RZipCode.colors = cbZipCode;
            return false;
        }

        else
        {
            cbZipCode.normalColor = Color.white;
            RtxtInfos.text = "";
            RZipCode.colors = cbZipCode;
        }
        #endregion

        #region Birth Month
        ColorBlock cbBirthMonth = birthmonth.colors;

        if (IsValidBirthMonth == false)
        {
            cbBirthMonth.normalColor = Color.red;
            RtxtInfos.text = "Select Your Birth Month";
            Handheld.Vibrate();
            birthmonth.colors = cbBirthMonth;
            return false;
        }

        else
        {
            cbBirthMonth.normalColor = Color.white;
            RtxtInfos.text = "";
            birthmonth.colors = cbBirthMonth;
        }
        #endregion

        #region Birth Day
        ColorBlock cbBirthDay = birthday.colors;

        if (IsValidBirthDay == false)
        {
            cbBirthDay.normalColor = Color.red;
            RtxtInfos.text = "Select Your Birth Day";
            Handheld.Vibrate();
            birthday.colors = cbBirthDay;
            return false;
        }

        else
        {
            cbBirthDay.normalColor = Color.white;
            RtxtInfos.text = "";
            birthday.colors = cbBirthDay;
        }
        #endregion

        #region Birth Year
        ColorBlock cbBirthYear = birthyear.colors;

        if (IsValidBirthYear == false)
        {
            cbBirthYear.normalColor = Color.red;
            RtxtInfos.text = "Select Your Birth Year";
            Handheld.Vibrate();
            birthyear.colors = cbBirthYear;
            return false;
        }

        else
        {
            cbBirthYear.normalColor = Color.white;
            RtxtInfos.text = "";
            birthyear.colors = cbBirthYear;
        }
        #endregion

        #region Profile
        ColorBlock cbProfile = profile.colors;

        if (IsValidProfile == false)
        {
            cbProfile.normalColor = Color.red;
            RtxtInfos.text = "Select Your Player Profile / Sélectionnez Votre Profil de Joueur";
            Handheld.Vibrate();
            profile.colors = cbProfile;
            return false;
        }

        else
        {
            cbProfile.normalColor = Color.white;
            RtxtInfos.text = "";
            profile.colors = cbProfile;
        }
        #endregion

        #region Terms
        ColorBlock cbReadTerms = readTerms.colors;

        if (IsValidTerms == false)
        {
            cbReadTerms.normalColor = Color.red;
            RtxtInfos.text = "You must accept the Terms to continue";
            Handheld.Vibrate();
            readTerms.colors = cbReadTerms;
            return false;
        }

        else
        {
            cbReadTerms.normalColor = Color.white;
            RtxtInfos.text = "";
            readTerms.colors = cbReadTerms;
        }
        #endregion

        RtxtInfos.text = null;
        return true;
    }

    public void Register()
    {
        if (IsValid())
        {
            string cmd = "INSERT INTO `users` (`id`, `pseudo`, `email`, `password`, `firstname`, `lastname`, `gender`, `phonenumber`, `country`, `address`, `city`, `zipcode`, `birthmonth`, `birthday`, `birthyear`, `profile`, `points`, `event1`, `event2`, `event3`, `event4`, `event5`) VALUES(NULL, '" + RPseudo.text + "', '" + REmail.text + "', '" + Md5Sum(RPassword.text) + "', '" + RFirstName.text + "', '" + RLastName.text + "', '" + selectedGender.text + "', '" + RPhoneNumber.text + "', '" + selectedCountry.text + "', '" + RAddress.text + "', '" + RCity.text + "', '" + RZipCode.text + "', '" + selectedBirthMonth.text + "', '" + selectedBirthDay.text + "','" + selectedBirthYear.text + "','" + selectedProfile.text + "','0','0','0','0','0','0')";
            MySqlCommand CmdSql = new MySqlCommand(cmd, con);
            try
            {
                CmdSql.ExecuteReader();
                RtxtInfos.text = "Register Succesful";
            }

            catch (Exception Ex)
            {
                RtxtInfos.text = Ex.ToString();
            }
        }
    }

    #region Cryptage Password
    string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
    #endregion

    public void Login()
    {
        try
        {
            connect_BDD();
            MySqlCommand CmdSql = new MySqlCommand("SELECT * FROM `users` WHERE `pseudo`='" + LPseudo.text + "'", con);
            MySqlDataReader MyReader = CmdSql.ExecuteReader();
            string data = null;
            
            while (MyReader.Read())
            {
                data = MyReader["password"].ToString();

                if (data == Md5Sum(LPassword.text))
                {
                    LtxtInfos.text = "Login Successful";
                    IPseudo = MyReader["pseudo"].ToString();
                    //IPoints = (int)MyReader["points"];
                    SceneManager.LoadScene("MainMenu");
                    IBelogin = true;
                    IPseudoSave = IPseudo;
                    IBeloginSave = IBelogin;
                }

                else
                {
                    LtxtInfos.text = "Wrong Pseudo Or Password";
                }
            }

            if (data==null)
            {
                LtxtInfos.text = "Account Not Existing";
            }
            MyReader.Close();
        }

        catch (Exception Ex) { Debug.Log(Ex.ToString()); }
    }

    #region Show Register or Login
    public void ShowRegister()
    {
        CanvasLogin.gameObject.SetActive(false);
        CanvasTerms.gameObject.SetActive(false);
        CanvasRegister.gameObject.SetActive(true);
    }

    public void ShowLogin()
    {
        CanvasLogin.gameObject.SetActive(true);
        CanvasRegister.gameObject.SetActive(false);
    }
    #endregion

    /*#region Save Points Database
    public void savePoints()
    {
        string cmd = "UPDATE `users` SET `points`=" + IPoints + " WHERE `pseudo`= '" + IPseudo + "'";
        MySqlCommand CmdSql = new MySqlCommand(cmd, con);

        try
        {
            CmdSql.ExecuteReader();
            Debug.Log("update successful");
        }

        catch (Exception Ex)
        {
            Debug.Log(Ex.ToString());
        }
    }
    #endregion*/

    /*#region Leaderboards
    public string LeaderBoard(int Limit)
    {
        try
        {
            connect_BDD();
            MySqlCommand CmdSql=new MySqlCommand("SELECT * FROM `users` order by `points` DESC LIMIT " +Limit, con);
            MySqlDataReader MyReader = CmdSql.ExecuteReader();

            string data = null;
            while (MyReader.Read())
            {
                data += MyReader["pseudo"].ToString() + ":" + MyReader["points"] + "\n";
            }
            MyReader.Close();
            return data;
        }

        catch
        {
            return null;
        }
    }
    #endregion*/

    public void PlayTampon()
    {
        IPseudo = IPseudoSave;
        IBelogin = IBeloginSave;
    }

    public void SaveBeLogin()
    {
        IBeloginSave = IBelogin;
        IPseudoSave = IPseudo;
    }
}