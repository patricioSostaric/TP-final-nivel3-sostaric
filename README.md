\# TP Final Nivel 3 - Sostaric Patricio



\## 🧩 Refactorización y Blindaje



Este proyecto fue desarrollado con \*\*.NET Framework 4.8\*\*, utilizando \*\*ASP.NET WebForms\*\*, \*\*ADO.NET\*\*, \*\*SQL Server\*\*, \*\*CSS\*\* y \*\*Bootstrap\*\* para el diseño y la experiencia de usuario.  

Cada módulo fue ajustado y refactorizado para lograr una aplicación \*\*lista para evaluación profesional\*\*, defensiva y coherente.



\### 🔎 Filtros

\- \*\*Filtro común (`txtFiltro`)\*\*

&nbsp; - Vacío → devuelve todos los artículos

&nbsp; - Texto → filtra automáticamente por nombre

&nbsp; - Mensajes claros y únicos para resultados vacíos o sesión caída



\- \*\*Filtro avanzado (`chkFiltroAvanzado`)\*\*

&nbsp; - Validaciones defensivas: campo vacío y precio inválido

&nbsp; - Eliminada duplicación de mensajes con `EmptyDataTemplate`

&nbsp; - Layout restaurado con proporciones originales y botones unificados



\### 🛡️ Validaciones

\- Centralizadas en la helper estática `Validacion`

\- Reutilización en todas las páginas para coherencia y mantenibilidad

\- Mensajes consistentes en toda la aplicación



\### ⭐ Favoritos y Compras

\- Lógica defensiva contra duplicados y sesiones nulas

\- Manejo de excepciones con redirección a `Error.aspx`

\- Encapsulación en métodos para claridad y orden



\## ⚙️ Tecnologías utilizadas

\- \*\*.NET Framework 4.8\*\*

\- \*\*ASP.NET WebForms\*\*

\- \*\*ADO.NET\*\*

\- \*\*SQL Server\*\*

\- \*\*CSS\*\*

\- \*\*Bootstrap\*\*

\- \*\*LINQ\*\*

\- \*\*C#\*\*

\- \*\*Git/GitHub\*\* para control de versiones



\## 🎯 Estado del Proyecto

Este commit marca el \*\*blindaje definitivo\*\* del proyecto:  

\- Filtros inteligentes y anti‑errores  

\- Validaciones coherentes y centralizadas  

\- Código limpio, refactorizado y defensivo  





