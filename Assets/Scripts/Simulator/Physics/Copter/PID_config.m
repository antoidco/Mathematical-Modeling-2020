sum = 0;
pi = 0.002;
pd = 2.5;
pp = 1.5;
dheight = 100;
height = 90;
dspeed = 0;
speed = 0;
sum = 0;
k = 1.85e-5;
dt = 0.01;
b = 1.14e-7;
H = [];

for i = 1:1000
   sum = sum + pi * (dheight - height);
   thrust = pp * (dheight - height) + pd * (dspeed - speed) + sum + 9.81;
   thrust = thrust - speed * b/12 - 9.81;
   speed = speed + thrust*dt;
   height = height + speed*dt;
   H = [H height];
end

plot(dt.*(1:1000), H);