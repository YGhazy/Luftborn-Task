# Front-End project structure
This is the full documentation about the project template structure.

# **App**

## **Enums** :
 * *Error Type*
 * Other Enums (Based on requirements)
## **Models** : 
+ Auth-models 
 + DTOs
 + Http-models
## **Modules** : 
 + ### Authentication-Module
>    - *Login/Register/etc...*
+ ### Layout-module
>    - *Children Components and/or modules*
 + ### Shared
>   - *Shared components*
>   - *Shared module (`imported shared/external packages`)*
>   - *Shared helper components*

## **Services** : 
+ Shared-Services
>    - API-Constants
>         - API End-Point URLs
>    - Base-service
>         - `Responsible for CRUD operations (get/post/put/delete)`
>    - Shared-Services
 + Http-Services
>   - Auth-Guard 
>   - Token-Interceptor
+ Services 
>   - `Component services (Based on requirements)`

# **Assets** :
 + *Images*
 + *Fonts*
 + *Javascript/JQuery files*
 + *Image Service Uploads*

# **Environments** :

 >*`API/Image Server links`*

+ *Environment-Prod*
 + *Environment*