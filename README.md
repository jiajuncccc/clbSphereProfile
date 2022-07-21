# tekla����������

tekla֧��ͨ��`*.clb`�ļ������Զ�������Ľ��棻
ͨ������`components.clb`�ļ������Զ�����棻
ͨ������`profitab.inp`�ļ���`tekla structures`����ʾ������Զ�����棻

## .clb�����ļ���ʽ

�������ͣ��û�����ģ��в����ġ�

�ļ�·����`..\ProgramData\Trimble\Tekla Structures\<version>\environments\common\inp`

```plantuml
@startmindmap
+ .clb�ļ�
++ library_id
++[#Orange] section_type(...) 
+++ name
+++[#Orange] base_attribute(...)
++++ name
++++ description
++++ type
++++ default
+++[#Orange] expression(...)
++++ name 
++++ type
++++ default
++++ formula
+++ geometry
++++ name
++++[#Orange] face(...)
+++++ index
+++++[#Orange] point(...)
++++++ value
++++++ x
++++++ y
@endmindmap
```

### library_id

�� profitab.inp �ļ���ʹ�õĿ�����,�磺

```
library_id "SJ"
```

### section_type

����������

#### name

�� profitab.inp �ļ���ʹ�õĽ������ƣ��磺
```
section_type
{
        name "SJSPHERE"
```

#### base_attribute

�������Ĳ������磺

```
base_attribute
{
name "d"
description "albl_Diameter"
type dimension
default 500
}
```

#### expression

�����ڴ˽��涨����ʹ�õı������磺

```
expression
{
name "p1"
type dimension
formula 0.140*d
}
```

* formult Ϊ�����ļ��㹫ʽ

��ʹ�ý���������Ѷ���ı�����
�Ѳ��Կ��õĺ����У�+ - * / sin cos tan PI Min Max

* ���涨���а����������ʱ������tekla structures����ᵼ�����������

��������tekla structuresʱ��������������100��

#### geometry

����ʵ�嶥���λ�ã��磺

```
geometry
{
    name "default"
    face
    {
        index 0
        point 0  y1  z1
        point 0  y1  z2
        point 0  y2  z2
        point 0  y4  z4
        point 0  y5  z3
        point 0  y3  z1
    }
}
```

* index : ��ı�ţ���0��ʼ�����������
* point.value : 0=������ʼ�㣬1=�����յ㡣
* point.x �������϶����xֵ
* point.y �������϶����yֵ

������������Ϊ�������½ǿ�ʼ������ʱ��˳��������㡣

### ע��

```
// this is a comment
```

## components.clb

�ļ�·����`..\ProgramData\Trimble\Tekla Structures\<version>\environments\common\inp`

```

//
// you can add your own library files here
//
include "Roundrect.clb"	// Circle to rectangle
include "RUNDRECHT.clb"	// Circle to rectangle

```

##  profitab.inp

�ļ�·��:` ..\ProgramData\Trimble\Tekla Structures\<version>\environments\<environment>\`

```

/* Others
/*-------+------+----+---+----+----+------------------+------------------+
BLKS     ! USER ! 0  !   !  2 !  2 !1Gen.Sleeve       !d1-d2
CAP      ! USER ! 0  !   !  1 !  1 !CS.CAP            !d
HEMISPHER! USER ! 0  !   !  1 !  1 !TfrLib.HEMISPHER  !d
NUT_M    ! USER ! 0  !   !  1 !  1 !1Gen.Nut          !d
SJSPHERE ! USER ! 0  !   !  1 !  1 !1Gen.SJSPHERE     !d 
SPHERE   ! USER ! 0  !   !  1 !  1 !1Gen.SPHERE       !d                 !

```