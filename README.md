# InternationalBusinessmen
Ejercicio de desarrollo en .NET Core

International Business Men
(Duración máxima: 4 horas)
Trabajas para el GNB (Gloiath National Bank), y tu jefe, Barney Stinson, te ha pedido que
diseñes e implementes una aplicación backend para ayudar a los ejecutivos de la empresa
que vuelan por todo el mundo. Los ejecutivos necesitan un listado de cada producto con el
que GNB comercia, y el total de la suma de las ventas de estos productos.
Para esta tarea debes crear un webservice. Este webservice puede devolver los resultados
en formato XML o en JSON. Eres libre de escoger el formato con el que te sientas más
cómodo (sólo es necesario que se implemente uno de los formatos).
Recursos a utilizar:
 http://quiet-stone-2094.herokuapp.com/rates.xml o http://quiet-stone-
2094.herokuapp.com/rates.json devuelve un documento con los siguientes formatos;
XML
&lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;
&lt;rates&gt;
&lt;rate from=&quot;EUR&quot; to=&quot;USD&quot; rate=&quot;1.359&quot;/&gt;
&lt;rate from=&quot;CAD&quot; to=&quot;EUR&quot; rate=&quot;0.732&quot;/&gt;
&lt;rate from=&quot;USD&quot; to=&quot;EUR&quot; rate=&quot;0.736&quot;/&gt;
&lt;rate from=&quot;EUR&quot; to=&quot;CAD&quot; rate=&quot;1.366&quot;/&gt;
&lt;/rates&gt;
JSON
[
{ &quot;from&quot;: &quot;EUR&quot;, &quot;to&quot;: &quot;USD&quot;, &quot;rate&quot;: &quot;1.359&quot; },
{ &quot;from&quot;: &quot;CAD&quot;, &quot;to&quot;: &quot;EUR&quot;, &quot;rate&quot;: &quot;0.732&quot; },
{ &quot;from&quot;: &quot;USD&quot;, &quot;to&quot;: &quot;EUR&quot;, &quot;rate&quot;: &quot;0.736&quot; },
{ &quot;from&quot;: &quot;EUR&quot;, &quot;to&quot;: &quot;CAD&quot;, &quot;rate&quot;: &quot;1.366&quot; }
]
Cada entrada en la colección especifica una conversión de una moneda a otra (cuando te
devuelve una conversión, la conversión contraria también se devuelve), sin embargo hay
algunas conversiones que no se devuelven, y en caso de ser necesarias, deberán ser
calculadas utilizando las conversiones que se dispongan. Por ejemplo, en el ejemplo no se
envía la conversión de USD a CAD, esta debe ser calculada usando la conversión USD a
EUR y después EUR a CAD.
 http://quiet-stone-2094.herokuapp.com/transactions.xml o http://quiet-stone-
2094.herokuapp.com/transactions.json devuelve un documento con los siguientes
formatos:
XML
&lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt; &lt;transactions&gt;
&lt;transaction sku=&quot;T2006&quot; amount=&quot;10.00&quot; currency=&quot;USD&quot;/&gt;
&lt;transaction sku=&quot;M2007&quot; amount=&quot;34.57&quot; currency=&quot;CAD&quot;/&gt;
&lt;transaction sku=&quot;R2008&quot; amount=&quot;17.95&quot; currency=&quot;USD&quot;/&gt;
&lt;transaction sku=&quot;T2006&quot; amount=&quot;7.63&quot; currency=&quot;EUR&quot;/&gt;
&lt;transaction sku=&quot;B2009&quot; amount=&quot;21.23&quot; currency=&quot;USD&quot;/&gt;
...
&lt;/transactions&gt;
JSON
[
{ &quot;sku&quot;: &quot;T2006&quot;, &quot;amount&quot;: &quot;10.00&quot;, &quot;currency&quot;: &quot;USD&quot; },
{ &quot;sku&quot;: &quot;M2007&quot;, &quot;amount&quot;: &quot;34.57&quot;, &quot;currency&quot;: &quot;CAD&quot; },

{ &quot;sku&quot;: &quot;R2008&quot;, &quot;amount&quot;: &quot;17.95&quot;, &quot;currency&quot;: &quot;USD&quot; },
{ &quot;sku&quot;: &quot;T2006&quot;, &quot;amount&quot;: &quot;7.63&quot;, &quot;currency&quot;: &quot;EUR&quot; },
{ &quot;sku&quot;: &quot;B2009&quot;, &quot;amount&quot;: &quot;21.23&quot;, &quot;currency&quot;: &quot;USD&quot; }
]
Cada entrada en la colección representa una transacción de un producto (el cual se
identifica mediante el campo SKU), el valor de dicha transacción (amount) y la moneda
utilizada (currency).
La aplicación debe tener un método desde el cuál se pueda obtener el listado de todas las
transacciones. Otro método con el que obtener todos los rates. Y por último un método al
que se le pase un SKU, y devuelva un listado con todas las transacciones de ese producto
en EUR, y la suma total de todas esas transacciones, también en EUR.
Por ejemplo, utilizando los datos anteriores, la suma total para el producto T2006 debería
ser 14,99.
Además necesitamos un plan B en caso que el webservice del que obtenemos la
información no esté disponible. Para ello, la aplicación debe persistir la información cada
vez que la obtenemos (eliminando y volviendo a insertar los nuevos datos). Puedes utilizar
el sistema que prefieras para ello.

Requisitos
Puedes utilizar cualquier framework y cualquier librería de terceros.
Recuerda que pueden faltar algunas conversiones, deberás calcularlas mediante la
información que tengas.
Separación de responsabilidades en distintas capas: Servicios distribuidos, capa de
aplicación, capa de dominio.
Implementación de log de error y manejo de excepciones en cada capa.
Tener en cuenta los principios SOLID y correcta capitalización del código.
Uso de Inyección de dependencias.

Puntos extra (No obligatorios)
Estamos tratando con divisas, intenta no utilizar números con coma flotante.
Después de cada conversión, el resultado debe ser redondeado a dos decimales usando
el redondeo Banker&#39;s Rounding
(http://en.wikipedia.org/wiki/Rounding#Round_half_to_even)
Por favor, el comentario del commit final ha de ser &quot;Finished&quot;, para informarnos de
que se ha finalizado la prueba.
