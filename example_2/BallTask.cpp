#include <fstream>
#include <iostream>
#include <cstdlib>
#include <cmath>
using namespace std;
void move(int *a, int t_end,int& x_end, int& y_end){
    double x=a[2];
    double y=a[3];
    double t=0;
    int x_move=a[4];
    int y_move=a[5];
    while(abs(t_end-t)>0.0001){
        t=t+0.0001;
        x=x+x_move*0.0001;
        y=y+y_move*0.0001;
        if ((abs(a[0]-x)<=0.001)|| (abs(0-x)<=0.001)||(x>=a[0])||(x<=0)) x_move=-x_move;
        else if ((abs(a[1]-y)<=0.001)|| (abs(0-y)<=0.001)||(y>=a[1])||(y<=0)) y_move=-y_move;
        if (((abs(0-x)<=0.001)&&(abs(0-y)<=0.001))||((abs(0-x)<=0.001)&&(abs(a[1]-y)<=0.001))
            ||((abs(a[0]-x)<=0.001)&&(abs(0-y)<=0.001))||((abs(a[0]-x)<=0.001)&&(abs(a[1]-y)<=0.001))
            ||((x<=0)&&(y<=0))||((x<=0)&&(y>=a[1]))||((x>=a[0])&&(y<=0))||((x>=a[0])&&(y>=a[1]))){
               x_move=-x_move;
               y_move=-y_move;
            }

    }
    x_end=floor(x+0.5);
    y_end=floor(y+0.5);
}
int test_n(char f[24]){
    ifstream input_f(f);
    string str; int t=0;
    while(getline(input_f,str)){
        t++;
    }
    input_f.close();
    return t;
}
void input(char f[24],int str,int *a){
    int len=str;
    char b[len][str];
    ifstream input_f(f);
    for(int i=0;i<6;i++){
        if (i % 2==0)input_f.getline(b[i],len-1,',');
        else input_f.getline(b[i],len-1,'\n');
        int p=atoi(b[i]);
        a[i]=p;
    }
    for(int i=6;i<str;i++){
        input_f.getline(b[i],len-1,'\n');
        int p=atoi(b[i]);
        a[i]=p;
    }
    input_f.close();
}
int main(){
    char f[24];
    cout<<"Input file: ";
    cin >> f;
    ifstream input_f(f);
    if (!input_f) return 1;
    int n;
    n =test_n(f);
    int *a=(int*)malloc((n+3)*sizeof(int));
    input(f,n+3,a);
    input_f.close();
    int x_end=0;
    int y_end=0;
    for( int i=6;i<n+3;i++){
        move(a,a[i],x_end,y_end);
        cout<<x_end<<","<<y_end<<endl;
    }
    return 0;
}

