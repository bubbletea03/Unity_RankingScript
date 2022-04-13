using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public InputField score; // 인풋필드 인스펙터창에서 받기
    public InputField id;

    private GameManager gameManager;

    private int[] scoreData;
    private string[] idData;
    private int dataSize = 0;

    private void Start(){

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //객체


        scoreData = new int[gameManager.textBar.Length]; // 최대 크기 : 텍스트바 수만큼까지만 받아줌
        idData = new string[gameManager.textBar.Length];
    }
    public void InputFieldButton(){
        if(dataSize < scoreData.Length){
            scoreData[dataSize] = System.Convert.ToInt32(score.text); // 인트형으로.
            idData[dataSize] = id.text;
            dataSize++; // 지금 배열 인덱스 어디까지 값 저장돼있는지.

            RankingDataUpdate();
            RankingTextUpdate();
        }
    }

    public void RankingDataUpdate(){ // 선택 정렬 (돌아서 최댓값을 맨앞으로, 돌아서 최댓값을 맨앞2번째로, 돌아서 최댓값을 맨앞3번째로 . . .)

        for(int i=0; i < dataSize-1; i++){
            int maxIndex = i; // 최댓값의 인덱스. 초기값 넣기
            for(int j=i+1; j < dataSize; j++){
                if(scoreData[j] > scoreData[maxIndex]){ // 최댓값보다 크다면 그 값을 최댓값으로
                    maxIndex = j;
                }
            }

            Swap<int>(scoreData ,i, maxIndex); // 맨앞자리랑 최댓값이랑 스왑
            Swap<string>(idData, i, maxIndex);
        }
    }

    public void RankingTextUpdate(){
        for(int i=0; i < dataSize; i++){
            gameManager.textBar[i].text = idData[i] + " : " + scoreData[i] + "점";
        }
    }

    public void Swap<T>(T[] array, int i, int j){ // Swap<배열의 자료형>(배열, 인덱스1, 인덱스2)
        T temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}
