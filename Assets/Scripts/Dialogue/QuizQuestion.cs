using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/QuizQuestion")]
public class QuizQuestion : ScriptableObject
{
    public Dialogue question;
    public Dialogue correct;
    public Dialogue wrong;
}
