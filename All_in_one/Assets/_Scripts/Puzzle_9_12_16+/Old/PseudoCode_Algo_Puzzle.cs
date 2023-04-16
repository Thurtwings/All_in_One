using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.PseudoCode
{
    public class PseudoCode_Algo_Puzzle
    {
        /*Jigsaw Puzzle
         * La grille:
         * 
         *      JigPuzzle.cs
         *      [S] int tilesAmount;
         *      
         *      
         *      for y < tilesAmount y++
         *          for x < tilesAmount x++
         *              
         *              create primitive quad
         *              set position
         *                   chaque quad fait 1*1 unit, le milieu est donc a .5f.
         *                   on démarre en bas à gauche, et toujours de gauche a droite
         *                   -V2.one(pour décaller le quad de 1 sur x * (tilesAmount -1 (ça commence à zéro)) * .5f(pour etre au centre du quad) + on décalle vers la droite, ou vers le haut, en fonction de x et y
         *              set parent(this)
         *              
         *              
         * La camera:
         *      [S] float multiplier
         *      modifier la taille ortho : xTilesAmount * multiplier
         *      sur pc (multiplier: .5f-.6f)
         *      sur mobile(multiplier:  
         *      
         *      
         *      
         *      
         * Les tuiles:
         *  
         *      étant des quads, et donc des objets 3D, ils disposent d'un mesh collider,
         *      ce qui les rend sensible aux events et autres actions, comme les clics
         *      cela évite d'ajouter les differentes interfaces IInputHandler fournies par Unity
         *      L'event Unity OnMouseDown permet d'enregistrer les clics de la souris, mais également les touch inputs.
         *      
         *      Tiles.cs
         *      
         *      public event Sys.Action<T> nomDeLeventPourSavoirSiOnAppuisSurLaTuile
         *      
         *      OnMouseDown(): nomDeLeventPourSavoirSiOnAppuisSurLaTuile?.Invoke(this)
         *      
         *      
         *     
         *      
         * */
    }
}
