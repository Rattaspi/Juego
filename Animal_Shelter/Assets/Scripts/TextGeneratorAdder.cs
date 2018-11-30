using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TextGeneratorAdder : EditorWindow {

    public OriginDataBase dataBase;
    public bool removeMode;

    public enum AffectableList { START, MID, END };
    public string stringToAdd;
    public int stringToRemoveIndex;
    public AffectableList affectedList;

    [MenuItem("Animal_Shelter/TextGeneratorAdder")]
    public static void Init() {
        TextGeneratorAdder window = (TextGeneratorAdder)EditorWindow.GetWindow(typeof(TextGeneratorAdder));
        window.name = "Text Generator Adder";
        window.Show();
    }

    private void OnGUI() {
        GUILayout.Label("Input");
        dataBase = (OriginDataBase)EditorGUILayout.ObjectField("Text Database", dataBase, typeof(OriginDataBase), true);
        affectedList = (AffectableList)EditorGUILayout.EnumPopup("List a afectar", affectedList);

        removeMode = EditorGUILayout.Toggle("Modo Quitar", removeMode);

        if (removeMode) {
            if (dataBase != null) {

                if(dataBase.originStart==null)
                dataBase.originStart = new List<string>();

                if (dataBase.originMiddle == null)
                    dataBase.originMiddle = new List<string>();

                if (dataBase.originEnd == null)
                    dataBase.originEnd = new List<string>();


                stringToRemoveIndex = EditorGUILayout.IntField("Index to remove", stringToRemoveIndex);
                switch (affectedList) {
                    case AffectableList.START:
                        if (dataBase.originStart.Count > stringToRemoveIndex) {
                            if (GUILayout.Button("Eliminar texto en indice " + stringToRemoveIndex.ToString())) {
                                dataBase.originStart.RemoveAt(stringToRemoveIndex);
                                dataBase.SetDirty();
                                AssetDatabase.SaveAssets();
                            }
                        } else {
                            GUILayout.Label("Índice no valido");
                        }
                        break;
                    case AffectableList.MID:
                        if (dataBase.originMiddle.Count > stringToRemoveIndex) {
                            if (GUILayout.Button("Eliminar texto en indice " + stringToRemoveIndex.ToString())) {
                                dataBase.originMiddle.RemoveAt(stringToRemoveIndex);
                                dataBase.SetDirty();
                                AssetDatabase.SaveAssets();
                            }
                        } else {
                            GUILayout.Label("Índice no valido");
                        }

                        break;
                    case AffectableList.END:
                        if (dataBase.originEnd.Count > stringToRemoveIndex) {
                            if (GUILayout.Button("Eliminar texto en indice " + stringToRemoveIndex.ToString())) {
                                dataBase.originEnd.RemoveAt(stringToRemoveIndex);
                                dataBase.SetDirty();
                                AssetDatabase.SaveAssets();
                            }
                        } else {
                            GUILayout.Label("Índice no valido");
                        }
                        break;
                }
            } else {
                GUILayout.Label("Asigna una base de datos");
            }
        } else {
            GUILayout.Label("Si no incluyes aquí una base de datos se creará una nueva");

            if (stringToAdd == null) {
                stringToAdd = "";
            }

            stringToAdd = EditorGUILayout.TextField("String a añadir: ", stringToAdd);


            if (stringToAdd.Length >=4) {
                if (GUILayout.Button("Añadir texto")) {
                    if (dataBase == null) {
                        dataBase = new OriginDataBase();

                        dataBase.originStart = new List<string>();
                        dataBase.originMiddle = new List<string>();
                        dataBase.originEnd = new List<string>();

                        string path = "Assets/Resources/OriginDataBases/";
                        AssetDatabase.CreateAsset(dataBase, path + "OriginDataBase.asset");

                    } else {
                        if (dataBase.originStart == null)
                            dataBase.originStart = new List<string>();

                        if (dataBase.originMiddle == null)
                            dataBase.originMiddle = new List<string>();

                        if (dataBase.originEnd == null)
                            dataBase.originEnd = new List<string>();
                    }

                    switch (affectedList) {
                        case AffectableList.START:
                            dataBase.originStart.Add(stringToAdd);
                            break;
                        case AffectableList.MID:
                            dataBase.originMiddle.Add(stringToAdd);
                            break;
                        case AffectableList.END:
                            dataBase.originEnd.Add(stringToAdd);
                            break;
                    }
                    dataBase.SetDirty();
                    AssetDatabase.SaveAssets();

                }
            } else{
                GUILayout.Label("Texto demasiado corto");
            }

        }


    }

}
