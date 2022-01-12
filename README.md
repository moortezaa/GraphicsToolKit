# GraphicsToolKit
this tool kit shall help .net developers to create simple graphics.

## Gradient Form
in this form user may create curves for each red, green and blue.

the curves can be used like this:
```
        public static Color GetPeriorityColor(float persent)
        {
            int x = (int)(persent / 100f * 582f);

            Point[] points = new Point[]{
                new Point(0,0),
                new Point(463,25),
                new Point(582,82),
                };
            int blue = (int)((float)GetYOverCurve(x, points) / 82f * 255f);

            points = new Point[]{
                new Point(0,0),
                new Point(311,61),
                new Point(582,82),
                };
            int green = (int)((float)GetYOverCurve(x, points) / 82f * 255f);

            points = new Point[]{
                new Point(0,0),
                new Point(82,64),
                new Point(582,82),
                };
            int red = (int)((float)GetYOverCurve(x, points) / 82f * 255f);

            var color = Color.FromArgb(red, green, blue);
            return color;
        }
```
x is where on the curve you want the color.
and peresent is the somthin optional that can be modified to favor.

in the form is a textBox witch allows a float from 0 to 1, and its porpose is to make the curve smooth or add tension to it.

in the left list boxes, if an item is selected then the next point added to panels on right will replace it.
