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
         *                   on d�marre en bas � gauche, et toujours de gauche a droite
         *                   -V2.one(pour d�caller le quad de 1 sur x * (tilesAmount -1 (�a commence � z�ro)) * .5f(pour etre au centre du quad) + on d�calle vers la droite, ou vers le haut, en fonction de x et y
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
         *      �tant des quads, et donc des objets 3D, ils disposent d'un mesh collider,
         *      ce qui les rend sensible aux events et autres actions, comme les clics
         *      cela �vite d'ajouter les differentes interfaces IInputHandler fournies par Unity
         *      L'event Unity OnMouseDown permet d'enregistrer les clics de la souris, mais �galement les touch inputs.
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
