- https://www.youtube.com/watch?v=pJb9O7foT-8

- https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/relationships

- https://www.learnentityframeworkcore.com/configuration/fluent-api

# Manejo de relaciones entre entidades utilizando Fluent API.
<br/>

## Introducci�n

**Entity Framework Core Fluent API** es una forma de configurar el comportamiento de las entidades y relaciones de **Entity Framework Core** usando una interface fluida, basada en codigo. En lugar de atributos de datos en las clases de modelo (*Data Annotations*), el Fluent API proporciona un m�todo m�s flexibley detallado para definir la configuraci�n de mapeo entre clases y la base de datos.

Algunos de los aspectos que se pueden configurar con el Fluent API incluye:

1. **Mapeo de entidades:** Definir cu�l clase de C# corresponde a qu� tabla en la base de datos.
2. **Relaciones:**  Configurar relaciones entre entidades, como *uno-a-uno* *uno-a-muchos* y *muchos-a-muchos*.
3. **Claves primarias y for�neas:** Especificar qu� propiedades son claves primarias y c�mo se gestionan las claves for�neas.
4. **Restricciones y propiedades:** Definir longitud maxima de cadenas, campos requeridos, valores predeterminados, �ndices, etc.
5. **Henrencia:** Configurar c�mo manejan las jerarqu�as de herencia entre entidades.
6. **Convenciones personalizadas:** Sobreescribir las convenciones predeterminadasde EF Core.

```C#
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurando la entidad Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products"); // Mapear a tabla Products
                entity.HasKey(p => p.ProductId); // Definir clave primaria
                entity.Property(p => p.Name)
                      .IsRequired()  // Campo obligatorio
                      .HasMaxLength(100); // Longitud m�xima de 100 caracteres
                entity.HasIndex(p => p.Name)  // Crear un �ndice en la columna Name
                      .IsUnique();  // Definir que el �ndice es �nico
            });
        }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }
```

## Relaciones en Entity Framework 

Entity Framework permite definir c�mo las entidades (tablas) se relacionan entre si en la base de datos. Las relaciones pueden ser de tres tipos:

#### Uno-a-muchos

Este es el tipo m�s comun de relaci�n en las bases de datos. Por ejemplo, una entidad **Blog** puede tener muchas **Post**, pero cada **Post** pertenece a un �nico **Blog**.

```C#
    modelBuilder.Entity<Post>()
        .HasRequired(p => p.Blog) // 'Post' requiere un 'Blog'
        .WithMany(b => b.Posts)   // 'Blog' tiene muchos 'Posts'
        .HasForeignKey(p => p.BlogID)  // Clave for�nea en 'Post'
```

- `HasForeignKey`: Especifica la clave for�nea que conecta las dos entidades. (`BlogId`)

#### Uno-a-uno 

Esta relacion uno a uno, cada fila de una tabla esta asociada con una �nica fila de otra tabla. Un ejemplo puede ser entre User y UserProfile, donde un Usuario tiene un perfil �nico.

```C#
    modelBuilder.Entity<User>()
        .HasOptional(u => u.UserProfile) // Un 'User' tiene opcionalmente un 'UserProfile'
        .WithRequired(up => up.User);     // Cada 'UserProfile' requiere un 'User'
```

#### Muchos-a-muchos

Este tipo de relaci�n implica que filas de una tabla pueden estar relacionadas con muchas filas de otra tabla. Por ejemplo, una relaci�n entre `Student` y `Course`, donde estudiante puede inscribirse entre muchos cursos, y un curso puede tener muchos estudiantes.

```
modelBuilder.Entity<Student>()
    .HasMany(s => s.Courses)         // Un 'Student' puede tener muchos 'Courses'
    .WithMany(c => c.Students)       // Un 'Course' puede tener muchos 'Students'
    .Map(cs =>                       // Configuraci�n de la tabla intermedia
    {
        cs.MapLeftKey("StudentId");
        cs.MapRightKey("CourseId");
        cs.ToTable("StudentCourse"); // Nombre de la tabla intermedia
    });
```
- `HasMany` y `WithMany`: Configura la relaci�n de muchos a muchos.
- `Map`: Se utiliza para configurar la tabla intermedia que manejar� la relaci�n entre ambas entidades, especificando las claves for�neas y el nombre de la tabla.


https://learn.microsoft.com/es-es/ef/core/modeling/