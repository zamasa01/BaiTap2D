using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.EventSystems;

public class RegistryCheck : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField phone;
    public Text username_err;
    public Text username_err2;
    public Text password_err;
    public Text phone_err;
    public bool isRightUsername = false;
    public bool isRightPassword = false;
    public bool isRightPhone = false;
    private string AddressDataFile = @"Assets\Data\AccountData.csv";
    List<user> ListUser = new List<user>();
    public Animator Animator1;
    public Animator Animator2;

    void Start()
    {
        ReadData(AddressDataFile);
        system = EventSystem.current;
        firstInput.Select();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            submitButton.onClick.Invoke();
            Debug.Log("Enter key is presed!!!");
        }
    }
    public void usernameCheck()
    {
        if(username.text.Length < 8 || username.text.Length > 15)
        {
            username_err.gameObject.SetActive(true);
            username_err2.gameObject.SetActive(false);
            isRightUsername = false;
        }
        else
        {
            username_err.gameObject.SetActive(false);
            username_err2.gameObject.SetActive(false);
            isRightUsername = true;
        }
    }
    public void phoneCheck()
    {
        if (phone.text.Length < 8 || phone.text.Length > 12)
        {
            phone_err.gameObject.SetActive(true);
            isRightPhone = false;
        }
        else
        {
            phone_err.gameObject.SetActive(false);
            isRightPhone = true;
        }
    }
    public void passwordCheck()
    {
        char[] SpecialChars = @"!@#$%^&*()_+{}|[];':\<>?,./~`".ToCharArray();
        bool SpecialCharsCheck = false;
        foreach (var item1 in password.text.ToCharArray())
        {
            foreach (var item2 in SpecialChars)
            {
                if(item1 == item2)
                {
                    SpecialCharsCheck = true;
                }
            }
        }
        if (SpecialCharsCheck && password.text.Any(char.IsLower) && password.text.Any(char.IsUpper) && password.text.Any(char.IsNumber))
        {
            password_err.gameObject.SetActive(false);
            isRightPassword = true;
        }
        else
        {
            password_err.gameObject.SetActive(true);
            isRightPassword = false;
        }
    }
    public void showPW()
    {
        if(password.contentType == TMP_InputField.ContentType.Password)
        {
            password.contentType = TMP_InputField.ContentType.Standard;
            password.Select();
        }
        else
        {
            password.contentType = TMP_InputField.ContentType.Password;
            password.Select();
        }
    }
    public void RegistryButton()
    {
        if(isRightUsername && isRightPassword && isRightPhone)
        {
            int check = 0;
            foreach (var item in ListUser)
            {
                if(username.text == item.username)
                {
                    check++;
                }
            }

            if(check == 0)
            {
                WriteData(AddressDataFile);
                Animator1.SetTrigger("EndScene_On");
                Animator2.SetTrigger("EndScene_On");
                Invoke("LoadSceneLogin", 2.2f);
            }
            else
            {
                username_err2.gameObject.SetActive(true);
                username_err.gameObject.SetActive(false);
            }
        }
    }
    private void LoadSceneLogin()
    {
        SceneManager.LoadScene("Login_Scene");
    }
    public class user
    {
        public string username;
        public string password;
        public string phone;
    }
    public void ReadData(string address)
    {
        StreamReader ReadFile = new StreamReader(address);
        string[] SplitData = ReadFile.ReadToEnd().Split(',','\n');
        for (int i = 0; i < ((SplitData.Length - 1) / 3); i++)
        {
            user a = new user();
            a.username = SplitData[(3 * i)];
            a.password = SplitData[1 + (3 * i)];
            a.phone = SplitData[2 + (3 * i)];
            ListUser.Add(a);
            //Debug.Log($"Username {i + 1}: " + a.username);
            //Debug.Log($"Password {i + 1}: " + a.password);
            //Debug.Log($"Phone {i + 1}: " + a.phone);
        }
        ReadFile.Close();
    }
    public void WriteData(string address)
    {
        StreamWriter WriteFile = new StreamWriter(address, true);
        WriteFile.WriteLine($"{username.text},{password.text},{phone.text}");
        WriteFile.Close();
        ReadData(AddressDataFile);
    }
}
