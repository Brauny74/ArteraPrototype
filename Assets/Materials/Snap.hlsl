#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED
void Snap_float(float Value, float Step, out float Out)
{
	Step = max(Step, 1e-6);
	Out = floor(Value / Step) * Step;
}
#endif