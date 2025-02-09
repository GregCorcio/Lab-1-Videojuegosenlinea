using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField] private MenuVisibility[] menus;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evita múltiples instancias
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantiene este objeto entre escenas
    }

    public void OpenMenuName(string menuName)
    {
        if (menus == null || menus.Length == 0) return;

        foreach (var menu in menus)
        {
            if (menu.nameMenu == menuName)
            {
                menu.Visible();
            }
            else if (menu.isOpen)
            {
                CloseMenu(menu);
            }
        }
    }

    public void OpenMenu(MenuVisibility menu)
    {
        if (menu == null) return;

        foreach (var m in menus)
        {
            if (m.isOpen)
            {
                CloseMenu(m);
            }
        }
        menu.Visible();
    }

    public void CloseMenu(MenuVisibility menu)
    {
        if (menu == null) return;
        menu.NoVisible();
    }
}



