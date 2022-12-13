using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor;
using TMPro;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class LoginCheck : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;
    public TMP_InputField username;
    public TMP_InputField password;
    public Toggle rememberPW;
    public Text username_err;
    public Text username_err2;
    public Text password_err;
    public bool isRightUsername = false;
    public bool isRightPassword = false;
    public string AddressDataFile = @"Assets\Data\AccountData.csv";
    public string AddresRememberFile = @"Assets\Data\AccountRemember.csv";
    List<user> ListUser = new List<user>();
    public Animator Animator1;
    public Animator Animator2;

    // Start is called before the first frame update
    void Start()
    {
        ReadData(AddressDataFile);
        username.text = new StreamReader(AddresRememberFile).ReadToEnd();
        system = EventSystem.current;
        firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft();
            if (previous != null)
            {
                previous.Select();
            }
            else
            {
                previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (previous != null)
                {
                    previous.Select();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if(next != null)
            {
                next.Select();
            }
            else
            {
                next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight();
                if (next != null)
                {
                    next.Select();
                }
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
            isRightUsername = true;
        }
    }
    public void passwordCheck()
    {
        if(password.text.Length > 0)
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
    public void LoginButton()
    {
        usernameCheck();
        passwordCheck();
        if(isRightUsername && isRightPassword)
        {
            bool check = true;
            foreach (var item in ListUser)
            {
                if(username.text == item.username)
                {
                    if(password.text == item.password)
                    {
                        if (rememberPW.isOn) { WriteData(AddresRememberFile); }
                        check = false;
                        Animator1.SetTrigger("EndScene_On");
                        Animator2.SetTrigger("MoveToSetting_On");
                        Invoke("SettingLoadScene", 2.2f);
                    }
                }
            }
            if (check)
            {
                username_err2.gameObject.SetActive(true);
                username_err.gameObject.SetActive(false);
                Debug.Log("Error account is on!!!");
            }
        }
    }
    public void RegistryButton()
    {
        Animator1.SetTrigger("EndScene_On");
        Animator2.SetTrigger("MoveToRegistry_On");
        Invoke("RegistryLoadScene", 1.8f);
    }
    private void RegistryLoadScene()
    {
        SceneManager.LoadScene("Registry_Scene");
    }
    private void SettingLoadScene()
    {
        SceneManager.LoadScene("Welcome_Scene");
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
            Debug.Log($"Username {i + 1}: " + a.username);
            Debug.Log($"Password {i + 1}: " + a.password);
            Debug.Log($"Phone {i + 1}: " + a.phone);
        }
        ReadFile.Close();
    }
    public void WriteData(string address)
    {
        StreamWriter WriteFile = new StreamWriter(address);
        WriteFile.Write($"{username.text}");
        WriteFile.Close();
    }
}
