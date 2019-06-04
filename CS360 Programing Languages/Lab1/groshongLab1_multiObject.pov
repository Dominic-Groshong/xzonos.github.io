/*
  DEVELOPER:: DOMINIC GROSHONG
  VERSION :: 1/20/19 
  CLASS :: CS360
*/

// Standard inludes
#include "colors.inc"
#include "shapes.inc"
#include "textures.inc" 
#include "glass.inc"
#include "woods.inc"

// camara
                          
                            
camera {
    angle 14
    location  <15.0 , 10 , 10.0>
    look_at   <-.50 , 1.25 , 0.0>
}                   

// light source
light_source{<1500,2500, 2500> color White}

/* skybox / ambiant color, the chrome reflection on the table and the water
makes means everything gets a blue color shift since its reflecting the sky_sphere */ 
sky_sphere{ 
    pigment{ 
        color rgb<.2,.5,.75>
    }
}

// table plane 
plane { 
    <0,1,0>, 0  
    texture{ 
         Chrome_Metal
    } 
}
      

// bowl cylinder 
#declare bowl = object { 
    
    // create cylinder     
    Round_Cylinder(<0,0,0>, <0,1.5,0>, 1 ,       0.10,   0) 
         
    texture{ T_Wood1     
        normal { wood 0.5 scale 0.05}
        finish { phong 1 } 
        rotate<0,0, 0> scale 0.5
    }
}
 

// sphere knockout    
#declare bowlKO =     
    // create sphere   
    sphere { <0,1.5,0>, .9
        
        //add primary wood grain
        texture{   
            pigment{ 
                wood turbulence 0.02 octaves 4 lambda 3
                scale 0.175  rotate <2, 2, 0> 
                color_map {
                    [0.0 color rgb <1.30, 0.99, 0.64>]
                    [0.5 color rgb <0.75, 0.39, 0.17>]
                    [0.7 color rgb <0.75, 0.39, 0.17>]
                    [1.0 color rgb <1.30, 0.99, 0.64>]
                }
            }
            
            rotate <0,0, 0>  scale 1  translate <0,0,0>
            } 
       
       // add alternate color wood grain    
       texture{   
            pigment{ 
                wood turbulence 0.02 octaves 4 lambda 2.8
                scale 0.2 rotate <2, 2, 0> 
                translate <0.0175, 0.0175, 0.0175>
                color_map {
                    [0.00 color rgbt <1.00, 0.97, 0.95, 1.30>]
                    [0.55 color rgbt <0.85, 0.85, 0.40, 0.70>]
                    [0.65 color rgbt <0.85, 0.85, 0.40, 0.70>]
                    [1.00 color rgbt <1.00,0.975, 0.95, 1.30>]
                }
            }
            
    
            rotate <0,0, 0>  scale 1  translate <0,0,0>
        } 
        
        finish {
            ambient .2
            diffuse .6
            specular .75
            roughness .1
        }
    
    }  
    
    
// create bowl by using the bowl object and cutting out the KO object      
difference{
    object{bowl}
    object{bowlKO}
}    
          
    
// glass cup                 
cone { <-2,0,0>,0.5,<-2,2.00,0>, .7 
    open
       
    material{ texture { Dark_Green_Glass } 
        interior{ I_Glass } 
    } 

    scale <1,1,1> rotate<0,0,0> translate<0,0,0>         
} 

// create intersection shape, two spheres overlapping to create a lense/water effect hovering over the bowl
intersection 
{   
    // forms the top of the lense and gives it texture
    sphere
    {
        <0,0,0> 2 
        material{ 
            texture 
            { 
                NBoldglass   
                normal 
                { 
                    ripples 3.5 
                    scale 0.043  
                    translate<0,1.0,0> 
                    rotate<90,0,0>
                }
                
                finish 
                { 
                    phong 1 
                    phong_size 10 
                    reflection
                    {  
                        0.15 
                    } 
                }
            }
            
            interior
            { 
                I_Glass 
            }
        }
        
    }                                   
    
    //forms the bottom dome of the lense
    sphere
    {
        <0,2.5,0> 1            
    }
}

