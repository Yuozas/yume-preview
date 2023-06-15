﻿public interface IPreliminarySetup
{
    const int DEFAULT_ORDER = 0;
    const int REGISTER = 1;
    const int TEST = 2;
    const int USE = 3;
    const int FINISH = 4;

    void Setup();
    int Order => DEFAULT_ORDER;
}
