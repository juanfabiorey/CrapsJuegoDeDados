// Juego del "Craps" (o el "Pase Inglés")
// Caso de estudio del método Random() y estructuras "enum" utilizando Microsoft C#.

// Consigna: investigue sobre las reglas del juego y codifique una aplicación
//           de consola que le permita jugar "craps" sin tener en cuenta el
//           sistema de apuestas.

// Investigación: El jugador lanza 2 dados de 6 caras. Cuando éstos se detienen, se suman
//           los puntos ("pepitas" en la jerga del juego) de las caras superiores.
//           Si la suma es 7 u 11 en la primera lanzada, el jugador gana la partida.
//           Si la suma es 2, 3 o 12 ("craps") en la primera lanzada, el jugador pierde
//           Si la suma es 4, 5, 6, 8, 9 o 10 en la primera lanzada, ésta suma se convierte
//             en el "punto" del jugador. Para ganar se debe continuar lanzando los dados
//             hasta igualar dicha suma. Pierde la partida si obtiene un 7 antes de obtener
//             su "punto". 

// Planteo personal de la solución:
//
// 1°: Defino las "enums" y las "instancias" necesarias
// 2°: Creo el método TirarDados()
// 3°: Escribo el código del juego dentro de "Main"
//      - Defino el estado del juego
//      - Inicializo mi puntaje inicial en cero
//      - Tiro los dados
//      - Evalúo el puntaje obtenido para ver si "gano", "pierdo" o "continúo"
//          (para esto último muestro mensaje de "punto", si no es 2, 3 o 12 el juego continúa)
//      - Defino el bucle con el "estado" del juego. En este caso, el juego
//      vá a correr de forma contínua hasta que "gane" o "pierda".
//      - Muestro "mensaje" si "gané" o "perdí"
// 4°: ¿funcionará todo?

namespace CrapsJuegoDeDados
{
    internal class Craps
    {
        // generador de números aleatorios para usar en el método
        // para tirar los dados
        private static Random numerosAleatorios = new Random();

        // enumeraciones con las constantes que representan el estado del juego
        private enum Estado { Continua, Gana, Pierde}

        // enumeraciones con los nombres o apodos de las distintas sumas de las
        // "pepitas" de las caras superiores de los 2 dados
        private enum NombreSumas
        {
            SnakeEyes = 2,
            Trey = 3,
            Seven = 7,
            YoLeven = 11,
            BoxCars = 12
        }


        // jugar una ronda o pase

        static void Main(string[] args)
        {
            // defino el estado incial del juego
            Estado estadoDelJuego = Estado.Continua;
            int punto = 0; // el "punto" de la primera tirada si no se pierde o gana

            int sumaDeDados = TirarDados(); // primer tirada

            // determino si el juego continúa, termina o marca "punto" en la primer tirada
            // es más indicado usar "switch" en vez de "if"
            switch ((NombreSumas) sumaDeDados)
            {
                case NombreSumas.Seven: // gano a la primera con 7 u 11 (2 casos)
                case NombreSumas.YoLeven:
                    estadoDelJuego = Estado.Gana;
                    break;
                case NombreSumas.SnakeEyes: // "craps", pierdo con 2, 3 o 12 (3 casos)
                case NombreSumas.Trey:
                case NombreSumas.BoxCars:
                    estadoDelJuego = Estado.Pierde;
                    break;
                default: // si, ninguna de los anteriores, en su defecto marca "punto" y lo muestra
                    estadoDelJuego = Estado.Continua; 
                    punto = sumaDeDados;
                    Console.WriteLine($"El \"punto\" del jugador es {punto}");
                    break;
            }

            // bucle que continúa el juego si no gano o pierdo a la primera tirada
            while (estadoDelJuego == Estado.Continua)
            {
                sumaDeDados = TirarDados(); // tiro otra vez

                // determino nuevamente el estado del juego
                if (sumaDeDados == punto) 
                {
                    estadoDelJuego = Estado.Gana;
                }
                else 
                {
                    if (sumaDeDados == (int) NombreSumas.Seven)
                    {
                        estadoDelJuego = Estado.Pierde;
                    }
                }
            }

            // muestro el mensaje si "gané" o "perdí"
            if (estadoDelJuego == Estado.Gana)
            {
                Console.WriteLine("¡El jugador ha ganado!");
            }
            else
            {
                Console.WriteLine("El jugador perdió. ¡La casa gana!");
            }
        }

        // método para sortear y sumar los dados de las "tiradas"
        private static int TirarDados()
        {
            // leo el valor de los dados
            int dado1 = numerosAleatorios.Next(1, 7);
            int dado2 = numerosAleatorios.Next(1, 7);

            int sumaDados = dado1 + dado2;

            // muestro mensaje del resultado
            Console.WriteLine($"El jugador sacó {dado1} + {dado2}, sumando un total de: {sumaDados}");
            return sumaDados; // retorno el resultado
        }
    }
}
