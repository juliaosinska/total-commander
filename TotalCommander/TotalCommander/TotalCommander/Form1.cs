using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Web;
using System.Collections;


namespace TotalCommander
{
    public partial class frmCommander : Form
    {
        private ListView AktywneOkno = null;
        private string AktywnaSciezka;
        private ListView NieAktywneOkno = null;
        private string NieAktywnaSciezka;
        private string sciezka1 = "C:\\";
        private string sciezka2 = "C:\\";
        private ListViewColumnSorter lvwColumnSorter1;
        private ListViewColumnSorter lvwColumnSorter2;


        public frmCommander()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e) //zaladowanie frmCommander
        {
            string[] dyski = Environment.GetLogicalDrives(); //pobranie dostepnych dyskow na komputerze

            //dodanie wartosci z tablicy dyski do comboboxow
            Dysk1.Items.AddRange(dyski);
            Dysk2.Items.AddRange(dyski);

            //ustawienie domyslnego dysku
            Dysk1.SelectedIndex = 0;
            Dysk2.SelectedIndex = 0;

            AktywneOkno = lvOkno1;
            AktywnaSciezka = sciezka1;
            NieAktywneOkno = lvOkno2;
            NieAktywnaSciezka = sciezka2;

            lvwColumnSorter1 = new ListViewColumnSorter();
            this.lvOkno1.ListViewItemSorter = lvwColumnSorter1;

            lvwColumnSorter2 = new ListViewColumnSorter();
            this.lvOkno2.ListViewItemSorter = lvwColumnSorter2;

            WyswietlZawartosc(AktywneOkno, AktywnaSciezka);
            WyswietlZawartosc(NieAktywneOkno, NieAktywnaSciezka);

            AktualizujSciezki();

            lvOkno1.ColumnClick += lvOkno1_ColumnClick;
            lvOkno2.ColumnClick += lvOkno2_ColumnClick;
            lvOkno1.MouseDoubleClick += lvOkno1_MouseDoubleClick;
            lvOkno2.MouseDoubleClick += lvOkno2_MouseDoubleClick;
            lvOkno1.KeyDown += lvOkno1_KeyDown;
            lvOkno2.KeyDown += lvOkno2_KeyDown;
            lvOkno1.AfterLabelEdit += lvOkno1_AfterLabelEdit;
            lvOkno2.AfterLabelEdit += lvOkno2_AfterLabelEdit;
            lvOkno1.DragEnter += lvOkno1_DragEnter;
            lvOkno2.DragEnter += lvOkno2_DragEnter;
            lvOkno1.DragEnter += lvOkno1_DragDrop;
            lvOkno2.DragEnter += lvOkno2_DragDrop;
            lvOkno1.ItemDrag += lvOkno1_ItemDrag;
            lvOkno2.ItemDrag += lvOkno2_ItemDrag;
        }

        private void LvOkno1_MouseDown(object? sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WyswietlZawartosc(ListView lv, string sciezka) //zaladowanie wartosci do kontrolek listview
        {
            lv.Items.Clear(); //wyczysczenie kontrolek listview
            try
            {
                DirectoryInfo katalog = new DirectoryInfo(sciezka);

                if (katalog.Parent != null)
                {
                    ListViewItem item = new ListViewItem("...", "<DIR>");
                    lv.Items.Add(item);
                }

                //iteracja przez katalogi
                foreach (var dir in katalog.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(dir.Name, "<DIR>");
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(dir.CreationTime.ToString());
                    lv.Items.Add(item);
                }

                //iteracja przez pliki
                foreach (var file in katalog.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add(file.Extension);
                    item.SubItems.Add(file.CreationTime.ToString());
                    item.SubItems.Add(file.Length.ToString());
                    lv.Items.Add(item);
                }

                //dostosowanie szerokosci kolumn
                foreach (ColumnHeader column in lv.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyst¹pi³ b³¹d podczas wyœwietlania zawartoœci: " + ex.Message, "B³¹d");
            }
        }

        private void lvOkno1_ColumnClick(object sender, ColumnClickEventArgs e) //obsluga klikniecia w naglowek kolumny listview
        {
            if (e.Column == lvwColumnSorter1.SortColumn)
            {
                if (lvwColumnSorter1.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter1.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter1.Order = SortOrder.Ascending;
                }
            }
            else
            {
                lvwColumnSorter1.SortColumn = e.Column;
                lvwColumnSorter1.Order = SortOrder.Ascending;
            }

            this.lvOkno1.Sort();
        }

        private void lvOkno2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter2.SortColumn)
            {
                if (lvwColumnSorter2.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter2.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter2.Order = SortOrder.Ascending;
                }
            }
            else
            {
                lvwColumnSorter2.SortColumn = e.Column;
                lvwColumnSorter2.Order = SortOrder.Ascending;
            }

            this.lvOkno2.Sort();
        }

        public class ListViewColumnSorter : IComparer
        {
            private int ColumnToSort;
            private SortOrder OrderOfSort;
            private CaseInsensitiveComparer ObjectCompare;

            public ListViewColumnSorter()
            {
                ColumnToSort = 0;

                OrderOfSort = SortOrder.None;

                ObjectCompare = new CaseInsensitiveComparer();
            }

            public int Compare(object x, object y)
            {
                int compareResult;
                ListViewItem listviewX, listviewY;

                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

                if (OrderOfSort == SortOrder.Ascending)
                {
                    return compareResult;
                }
                else if (OrderOfSort == SortOrder.Descending)
                {
                    return (-compareResult);
                }
                else
                {
                    return 0;
                }
            }

            public int SortColumn
            {
                set
                {
                    ColumnToSort = value;
                }
                get
                {
                    return ColumnToSort;
                }
            }

            public SortOrder Order
            {
                set
                {
                    OrderOfSort = value;
                }
                get
                {
                    return OrderOfSort;
                }
            }

        }


        private void Dysk1_SelectedIndexChanged(object sender, EventArgs e) //aktualizacja zawartosci listview po zmianie dysku1
        {
            string wybranyDysk = Dysk1.SelectedItem.ToString();
            AktywnaSciezka = wybranyDysk;
            AktualizujSciezki();
            WyswietlZawartosc(AktywneOkno, AktywnaSciezka);
        }

        private void Dysk2_SelectedIndexChanged(object sender, EventArgs e) //aktualizacja zawartosci listview po zmianie dysku2
        {
            string wybranyDysk = Dysk2.SelectedItem.ToString();
            NieAktywnaSciezka = wybranyDysk;
            AktualizujSciezki();
            WyswietlZawartosc(NieAktywneOkno, NieAktywnaSciezka);
        }


        private void lvOkno1_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvOkno1.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = lvOkno1.SelectedItems[0];
                    string selectedPath = Path.Combine(AktywnaSciezka, selectedItem.Text);

                    if (selectedItem.Text == "...") //sprawdzenie czy wybrany element to folder nadrzedny
                    {
                        DirectoryInfo parentDirectory = Directory.GetParent(AktywnaSciezka); //wchodzenie do nadzrednego folderu
                        if (parentDirectory != null)
                        {
                            AktywnaSciezka = parentDirectory.FullName;
                            AktualizujSciezki();
                            WyswietlZawartosc(AktywneOkno, AktywnaSciezka);
                        }
                    }
                    else if (IsFolder(selectedItem)) //sprawdzenie czy wybrany element to folder
                    {
                        AktywnaSciezka = selectedPath;
                        AktualizujSciezki();
                        WyswietlZawartosc(AktywneOkno, AktywnaSciezka);
                    }
                    else //otwieranie plikow w domyslnym edytorze
                    {
                        string notepadPlusPlusPath = @"C:\Program Files\Notepad++\notepad++.exe";
                        AktualizujSciezki();
                        Process.Start(notepadPlusPlusPath, selectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyst¹pi³ b³¹d podczas uzyskiwania dostêpu do podfolderu/pliku: " + ex.Message, "B³¹d");
            }
        }

        private void lvOkno2_MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvOkno2.SelectedItems.Count > 0)
                {
                    ListViewItem selectedItem = lvOkno2.SelectedItems[0];
                    string selectedPath = Path.Combine(NieAktywnaSciezka, selectedItem.Text);

                    if (selectedItem.Text == "...")
                    {
                        DirectoryInfo parentDirectory = Directory.GetParent(NieAktywnaSciezka);
                        if (parentDirectory != null)
                        {
                            NieAktywnaSciezka = parentDirectory.FullName;
                            AktualizujSciezki();
                            WyswietlZawartosc(NieAktywneOkno, NieAktywnaSciezka);
                        }
                    }
                    else if (IsFolder(selectedItem))
                    {
                        NieAktywnaSciezka = selectedPath;
                        AktualizujSciezki();
                        WyswietlZawartosc(NieAktywneOkno, NieAktywnaSciezka);
                    }
                    else
                    {
                        string notepadPlusPlusPath = @"C:\Program Files\Notepad++\notepad++.exe";
                        AktualizujSciezki();
                        Process.Start(notepadPlusPlusPath, selectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyst¹pi³ b³¹d podczas uzyskiwania dostêpu do podfolderu/pliku: " + ex.Message, "B³¹d");
            }
        }

        private bool IsFolder(ListViewItem item) //sprawdzanie czy elemnt jest folderem
        {
            string fileType = item.SubItems[1].Text;
            return fileType == "<DIR>";
        }

        private void AktualizujSciezki() //aktualizacja sciezek w textboxach
        {
            tbAktywnaSciezka.Text = AktywnaSciezka;
            tbNieAktywnaSciezka.Text = NieAktywnaSciezka;
        }

        private void lvOkno1_KeyDown(object sender, KeyEventArgs e) //obsluga zdarzen wcisniecia klawiszy
        {
            if (e.KeyCode == Keys.F8)
            {
                UsunZaznaczonePlikiKatalogi(lvOkno1, AktywnaSciezka);
            }
            else if (e.KeyCode == Keys.F7)
            {
                DodajNowyKatalog(lvOkno1, AktywnaSciezka);
            }
        }

        private void lvOkno2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                UsunZaznaczonePlikiKatalogi(lvOkno2, NieAktywnaSciezka);
            }
            else if (e.KeyCode == Keys.F7)
            {
                DodajNowyKatalog(lvOkno2, NieAktywnaSciezka);
            }
        }

        private void UsunZaznaczonePlikiKatalogi(ListView lv, string sciezka) //obsluga zdarzenia wcisniecia f8 - usuniecie pliku/folderu
        {
            try
            {
                DialogResult result = MessageBox.Show("Czy na pewno chcesz usun¹æ zaznaczony plik/folder?", "Potwierdzenie usuwania", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lv.SelectedItems)
                    {
                        string nazwaPliku = item.Text;
                        string pelnaSciezka = Path.Combine(sciezka, nazwaPliku);

                        if (IsFolder(item))
                        {
                            Directory.Delete(pelnaSciezka, true);
                        }
                        else
                        {
                            File.Delete(pelnaSciezka);
                        }

                        lv.Items.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyst¹pi³ b³¹d podczas usuwania plików: " + ex.Message, "B³¹d");
            }

        }


        private void DodajNowyKatalog(ListView lv, string sciezka)
        {
            try
            {
                string nowyKatalog = "Nowy Katalog"; //domyslna nazwa

                //sprawdzenie czy nazwa "Nowy Katalog" jest ju¿ zajeta, jesli tak, dodaje liczby na koñcu nazwy
                int numer = 1;
                string nazwaKatalogu = nowyKatalog;
                while (Directory.Exists(Path.Combine(sciezka, nazwaKatalogu)))
                {
                    nazwaKatalogu = $"{nowyKatalog} ({numer++})";
                }

                //stworzenie sciezki z nowym katalogiem i utworzenie tego katalogu
                string pelnaSciezka = Path.Combine(sciezka, nazwaKatalogu);
                Directory.CreateDirectory(pelnaSciezka);

                //odlaczenie obslug zdarzen AfterLabelEdit
                lv.AfterLabelEdit -= lvOkno1_AfterLabelEdit;
                lv.AfterLabelEdit -= lvOkno2_AfterLabelEdit;

                //dodanie elementu do listview
                ListViewItem item = new ListViewItem(nazwaKatalogu, "<DIR>");
                item.SubItems.Add("<DIR>");
                item.SubItems.Add(DateTime.Now.ToString());
                lv.Items.Add(item);

                item.Selected = true;
                item.Focused = true;
                lv.Focus();
                lv.EnsureVisible(item.Index);

                //wlaczenie edycji nazwy nowego katalogu
                item.BeginEdit();

                //ponowne zarejestrowanie obslug zdarzen AfterLabelEdit
                lv.AfterLabelEdit += lvOkno1_AfterLabelEdit;
                lv.AfterLabelEdit += lvOkno2_AfterLabelEdit;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wyst¹pi³ b³¹d podczas tworzenia nowego katalogu: " + ex.Message, "B³¹d");
            }
        }

        private void lvOkno1_AfterLabelEdit(object sender, LabelEditEventArgs e) //aktualizacja parametrow po zmianie nazwy
        {
            if (e.Label != null)
            {
                string oldName = lvOkno1.Items[e.Item].Text;
                string newName = e.Label;

                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("Nazwa pliku/folderu nie mo¿e byæ pusta.", "B³¹d");
                    e.CancelEdit = true;
                    return;
                }

                string oldPath = Path.Combine(AktywnaSciezka, oldName);
                string newPath = Path.Combine(AktywnaSciezka, newName);

                try
                {
                    if (IsFolder(lvOkno1.Items[e.Item]))
                    {
                        Directory.Move(oldPath, newPath);
                    }
                    else
                    {
                        File.Move(oldPath, newPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wyst¹pi³ b³¹d podczas zmiany nazwy pliku/folderu: " + ex.Message, "B³¹d");
                    e.CancelEdit = true;
                    return;
                }

                lvOkno1.Items[e.Item].Text = newName;
                WyswietlZawartosc(AktywneOkno, AktywnaSciezka);
            }
        }

        private void lvOkno2_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                string oldName = lvOkno2.Items[e.Item].Text;
                string newName = e.Label;

                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("Nazwa pliku/folderu nie mo¿e byæ pusta.", "B³¹d");
                    e.CancelEdit = true;
                    return;
                }

                string oldPath = Path.Combine(NieAktywnaSciezka, oldName);
                string newPath = Path.Combine(NieAktywnaSciezka, newName);

                try
                {
                    if (IsFolder(lvOkno2.Items[e.Item]))
                    {
                        Directory.Move(oldPath, newPath);
                    }
                    else
                    {
                        File.Move(oldPath, newPath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wyst¹pi³ b³¹d podczas zmiany nazwy pliku/folderu: " + ex.Message, "B³¹d");
                    e.CancelEdit = true;
                    return;
                }

                lvOkno2.Items[e.Item].Text = newName;
                WyswietlZawartosc(NieAktywneOkno, NieAktywnaSciezka);
            }
        }

        private void lvOkno1_ItemDrag(object sender, ItemDragEventArgs e) //obsluguje przeciaganie z lvOKno1
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>(); //przechowuje zaznaczone elementy

            foreach (ListViewItem item in lvOkno1.SelectedItems)
            {
                selectedItems.Add(item);
            }

            List<string> draggedItemsPaths = new List<string>(); //przechowuje pelne sciezki do zaznaczonych elementow

            foreach (ListViewItem item in selectedItems)
            {
                string itemPath = Path.Combine(AktywnaSciezka, item.Text);
                draggedItemsPaths.Add(itemPath);
            }

            DataObject dataObject = new DataObject(); //tworzymy obiekt DataObject, sluzy do przenoszenia danych w przeciaganiu
            dataObject.SetData("ListViewItems", selectedItems); //dane "ListViewItems" zawieraja zaznaczone elementy
            dataObject.SetData("FileDrop", draggedItemsPaths.ToArray()); //dane "File Drop" sa tablica sciezek do zaznaczonych elementow 

            lvOkno1.DoDragDrop(dataObject, DragDropEffects.Copy);
        }

        private void lvOkno2_DragEnter(object sender, DragEventArgs e) //obsluga wejscia przeciagania na lvOkno2
        {
            if (e.Data.GetDataPresent("ListViewItems") || e.Data.GetDataPresent(DataFormats.FileDrop)) //sprawdzanie czy ktorys z typow danych jest dostepny
            {
                e.Effect = DragDropEffects.Copy; //efekt przeciagania -> kopiowanie
            }
        }

        private void lvOkno2_DragDrop(object sender, DragEventArgs e) //obsluga upuszczania elementow na lvOkno2
        {
            if (e.Data.GetDataPresent("ListViewItems")) //dane z lvOkno1
            {
                List<ListViewItem> draggedItems = (List<ListViewItem>)e.Data.GetData("ListViewItems");

                foreach (ListViewItem item in draggedItems)
                {
                    string sourceFilePath = Path.Combine(AktywnaSciezka, item.Text);
                    string destFilePath = Path.Combine(NieAktywnaSciezka, item.Text);

                    if (File.Exists(sourceFilePath))
                    {
                        try
                        {
                            File.Copy(sourceFilePath, destFilePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Wyst¹pi³ b³¹d podczas kopiowania pliku: " + ex.Message, "B³¹d");
                        }
                    }
                    else if (Directory.Exists(sourceFilePath))
                    {
                        try
                        {
                            KopiujFolder(sourceFilePath, Path.Combine(NieAktywnaSciezka, item.Text), true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Wyst¹pi³ b³¹d podczas kopiowania folderu: " + ex.Message, "B³¹d");
                        }
                    }
                }

                WyswietlZawartosc(lvOkno2, NieAktywnaSciezka);
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in draggedFiles)
                {
                    if (Directory.Exists(file))
                    {
                        string folderName = Path.GetFileName(file);
                        string destinationPath = Path.Combine(NieAktywnaSciezka, folderName);

                        try
                        {
                            KopiujFolder(file, destinationPath, true);
                            WyswietlZawartosc(lvOkno2, NieAktywnaSciezka);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Wyst¹pi³ b³¹d podczas kopiowania folderu: " + ex.Message, "B³¹d");
                        }
                    }
                }
            }
        }

        private void lvOkno2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            List<ListViewItem> selectedItems = new List<ListViewItem>();

            foreach (ListViewItem item in lvOkno2.SelectedItems)
            {
                selectedItems.Add(item);
            }
            List<string> draggedItemsPaths = new List<string>();
            foreach (ListViewItem item in selectedItems)
            {
                string itemPath = Path.Combine(NieAktywnaSciezka, item.Text);
                draggedItemsPaths.Add(itemPath);
            }

            DataObject dataObject = new DataObject();
            dataObject.SetData("ListViewItems", selectedItems);
            dataObject.SetData("FileDrop", draggedItemsPaths.ToArray());

            lvOkno1.DoDragDrop(dataObject, DragDropEffects.Copy);
        }

        private void lvOkno1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("ListViewItems") || e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lvOkno1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("ListViewItems"))
            {
                List<ListViewItem> draggedItems = (List<ListViewItem>)e.Data.GetData("ListViewItems");

                foreach (ListViewItem item in draggedItems)
                {
                    string sourceFilePath = Path.Combine(NieAktywnaSciezka, item.Text);
                    string destFilePath = Path.Combine(AktywnaSciezka, item.Text);

                    if (File.Exists(sourceFilePath))
                    {
                        try
                        {
                            File.Copy(sourceFilePath, destFilePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Wyst¹pi³ b³¹d podczas kopiowania pliku: " + ex.Message, "B³¹d");
                        }
                    }
                    else if (Directory.Exists(sourceFilePath))
                    {
                        try
                        {
                            KopiujFolder(sourceFilePath, Path.Combine(AktywnaSciezka, item.Text), true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Wyst¹pi³ b³¹d podczas kopiowania folderu: " + ex.Message, "B³¹d");
                        }
                    }
                }

                WyswietlZawartosc(lvOkno1, AktywnaSciezka);
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] draggedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in draggedFiles)
                {
                    if (Directory.Exists(file))
                    {
                        string folderName = Path.GetFileName(file);
                        string destinationPath = Path.Combine(AktywnaSciezka, folderName);

                        try
                        {
                            KopiujFolder(file, destinationPath, true);
                            WyswietlZawartosc(lvOkno1, AktywnaSciezka);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Wyst¹pi³ b³¹d podczas kopiowania folderu: " + ex.Message, "B³¹d");
                        }
                    }
                }
            }
        }

        private static void KopiujFolder(string sourceDirName, string destDirName, bool copySubDirs)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirName); //tworzy folder z informacjami o kopiowanym folderze

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException("Nie mo¿na odnaleŸæ Ÿród³owego folderu.");
                }

                Directory.CreateDirectory(destDirName);

                FileInfo[] files = dir.GetFiles(); //tablica zawoerajaca informacje o plikach w folderze
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                }

                if (copySubDirs) //kopiowanie podfolderow
                {
                    DirectoryInfo[] dirs = dir.GetDirectories();

                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        KopiujFolder(subdir.FullName, temppath, copySubDirs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyst¹pi³ b³¹d podczas kopiowania folderu: " + ex.Message, "B³¹d");
            }
        }

        private void btnStworz_Click(object sender, EventArgs e)
        {

        }

        private void btnUsun_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}



